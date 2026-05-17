using MediatR;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongGym.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QuanLyPhongGym.Application.Features.HoiVienManagement.Queries.GetById
{
    public class GetHoiVienByIdQueryHandler : IRequestHandler<GetHoiVienByIdQuery, object?>
    {
        private readonly IApplicationDbContext _context;

        public GetHoiVienByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<object?> Handle(GetHoiVienByIdQuery request, CancellationToken cancellationToken)
        {
            // Tìm chính xác hội viên theo Id được truyền tới
            var hoiVien = await _context.HoiViens
                .Where(h => h.Id == request.Id)
                .Select(h => new
                {
                    h.Id,
                    h.MemberCode,
                    h.LastName,
                    h.FirstName,
                    h.Phone,
                    h.Email,
                    h.FaceData, // Chuỗi dữ liệu ảnh phục vụ cho AI Camera nhận diện
                    h.CreatedDate,
                    h.Status
                })
                .FirstOrDefaultAsync(cancellationToken);

            return hoiVien;
        }
    }
}