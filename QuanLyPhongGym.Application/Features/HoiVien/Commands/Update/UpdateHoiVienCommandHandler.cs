using MediatR;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongGym.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace QuanLyPhongGym.Application.Features.HoiVien.Commands.Update
{
    public class UpdateHoiVienCommandHandler : IRequestHandler<UpdateHoiVienCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public UpdateHoiVienCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateHoiVienCommand request, CancellationToken cancellationToken)
        {
            // Tìm hội viên cần sửa trong Database
            var hoiVien = await _context.HoiViens
                .FirstOrDefaultAsync(h => h.Id == request.Id, cancellationToken);

            if (hoiVien == null) return false; // Không tìm thấy để sửa

            // Gán dữ liệu mới thay thế dữ liệu cũ
            hoiVien.MemberCode = request.MemberCode;
            hoiVien.LastName = request.LastName;
            hoiVien.FirstName = request.FirstName;
            hoiVien.Phone = request.Phone;
            hoiVien.Email = request.Email;
            hoiVien.Status = request.Status;

            // Lưu thay đổi vào SQL Server
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}