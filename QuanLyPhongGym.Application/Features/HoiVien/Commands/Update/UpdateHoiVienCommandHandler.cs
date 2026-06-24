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
            var hoiVien = await _context.HoiViens
                .FirstOrDefaultAsync(h => h.Id == request.Id, cancellationToken);

            if (hoiVien == null) return false;

            // Cập nhật các trường thông tin cơ bản
            hoiVien.LastName = request.LastName;
            hoiVien.FirstName = request.FirstName;
            hoiVien.Phone = request.Phone;
            hoiVien.Email = request.Email;
            hoiVien.Status = request.Status;

            // QUAN TRỌNG: Chỉ cập nhật MemberCode nếu request có gửi lên giá trị mới
            if (!string.IsNullOrEmpty(request.MemberCode))
            {
                hoiVien.MemberCode = request.MemberCode;
            }
            // Nếu request.MemberCode là null hoặc rỗng, 
            // chúng ta không đụng đến nó (giữ nguyên giá trị cũ trong Database)

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}