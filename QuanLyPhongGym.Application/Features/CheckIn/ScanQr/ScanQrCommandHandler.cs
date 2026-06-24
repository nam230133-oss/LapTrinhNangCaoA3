using MediatR;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongGym.Application.Common.Interfaces;
using QuanLyPhongGym.Domain.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QuanLyPhongGym.Application.Features.CheckIn.Commands.ScanQr
{
    public class ScanQrCommandHandler : IRequestHandler<ScanQrCommand, ScanQrResponse>
    {
        private readonly IApplicationDbContext _context;

        public ScanQrCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ScanQrResponse> Handle(ScanQrCommand request, CancellationToken cancellationToken)
        {
            // 1. Chuẩn hóa mã hội viên (loại bỏ khoảng trắng thừa)
            var code = request.MemberCode?.Trim();

            if (string.IsNullOrEmpty(code))
                return new ScanQrResponse { IsSuccess = false, Message = "Mã QR không hợp lệ!" };

            // 2. Tìm hội viên theo MemberCode
            var hoiVien = await _context.HoiViens
                .FirstOrDefaultAsync(h => h.MemberCode == code, cancellationToken);

            if (hoiVien == null)
                return new ScanQrResponse { IsSuccess = false, Message = "Không tìm thấy hội viên với mã này!" };

            // 3. Kiểm tra trạng thái hội viên (Chỉ cho phép Active)
            if (hoiVien.Status != "Active")
                return new ScanQrResponse { IsSuccess = false, Message = $"Hội viên không ở trạng thái kích hoạt (Trạng thái hiện tại: {hoiVien.Status})" };

            // 4. Kiểm tra chống spam (nếu check-in cách nhau dưới 5 phút)
            var lastCheckIn = await _context.CheckIns
                .Where(c => c.HoiVienId == hoiVien.Id)
                .OrderByDescending(c => c.CheckInTime)
                .FirstOrDefaultAsync(cancellationToken);

            if (lastCheckIn != null && (DateTime.UtcNow - lastCheckIn.CheckInTime).TotalMinutes < 5)
            {
                return new ScanQrResponse { IsSuccess = false, Message = "Vui lòng không quét mã liên tục trong vòng 5 phút!" };
            }

            // 5. Ghi log check-in
            var log = new QuanLyPhongGym.Domain.Entities.CheckIn
            {
                Id = Guid.NewGuid(),
                HoiVienId = hoiVien.Id,
                CheckInTime = DateTime.UtcNow
            };

            _context.CheckIns.Add(log);
            await _context.SaveChangesAsync(cancellationToken);

            return new ScanQrResponse
            {
                IsSuccess = true,
                Message = "Check-in thành công!",
                MemberName = $"{hoiVien.FirstName} {hoiVien.LastName}"
            };
        }
    }
}