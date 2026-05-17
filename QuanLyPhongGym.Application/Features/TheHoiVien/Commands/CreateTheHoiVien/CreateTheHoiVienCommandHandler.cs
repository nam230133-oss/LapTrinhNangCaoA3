using MediatR;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongGym.Application.Common.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QuanLyPhongGym.Application.Features.TheHoiVien.Commands.CreateTheHoiVien
{
    public class CreateTheHoiVienCommandHandler : IRequestHandler<CreateTheHoiVienCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateTheHoiVienCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateTheHoiVienCommand request, CancellationToken cancellationToken)
        {
            // 1. Kiểm tra xem Hội viên có tồn tại trong hệ thống không
            var hoiVienExists = await _context.HoiViens
                .AnyAsync(h => h.Id == request.HoiVienId, cancellationToken);

            if (!hoiVienExists)
            {
                throw new Exception("Không tìm thấy hội viên này trong hệ thống để đăng ký gói!");
            }

            // 2. Tìm thông tin Gói tập để lấy số ngày thời hạn (ThoiHanNgay)
            var goiTap = await _context.GoiTaps
                .Where(g => g.Id == request.GoiTapId)
                .FirstOrDefaultAsync(cancellationToken);

            if (goiTap == null)
            {
                throw new Exception("Gói tập được chọn không tồn tại!");
            }

            // 3. Tính toán ngày kích hoạt và ngày hết hạn tự động
            var ngayKichHoat = DateTime.Now;
            var ngayHetHan = ngayKichHoat.AddDays(goiTap.ThoiHanNgay); // Ví dụ: +30 ngày, +90 ngày

            // 4. Khởi tạo đối tượng Thẻ hội viên mới
            var theMoi = new Domain.Entities.TheHoiVien
            {
                Id = Guid.NewGuid(),
                HoiVienId = request.HoiVienId,
                GoiTapId = request.GoiTapId,
                NgayKichHoat = ngayKichHoat,
                NgayHetHan = ngayHetHan,
                TrangThai = true // Thẻ vừa mua sẽ ở trạng thái Kích hoạt luôn
            };

            // 5. Lưu xuống cơ sở dữ liệu SQL Server
            _context.TheHoiViens.Add(theMoi);
            await _context.SaveChangesAsync(cancellationToken);

            // 6. Trả về ID của thẻ vừa tạo
            return theMoi.Id;
        }
    }
}