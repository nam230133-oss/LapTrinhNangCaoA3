using MediatR;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongGym.Application.Common.Interfaces;
// Chỉ định rõ ràng class HoiVien từ Domain
using HoiVienEntity = QuanLyPhongGym.Domain.Entities.HoiVien;

namespace QuanLyPhongGym.Application.Features.HoiVien.Queries.GetList
{
    public class GetHoiVienListQueryHandler : IRequestHandler<GetHoiVienListQuery, List<HoiVienEntity>>
    {
        private readonly IApplicationDbContext _context;

        public GetHoiVienListQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<HoiVienEntity>> Handle(GetHoiVienListQuery request, CancellationToken cancellationToken)
        {
            return await _context.HoiViens
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}