using MediatR;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongGym.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

// Đảm bảo namespace này TRÙNG với file Query ở trên
namespace QuanLyPhongGym.Application.Features.HoiVienManagement.Queries.GetList
{
    public class GetHoiVienListQueryHandler : IRequestHandler<GetHoiVienListQuery, List<QuanLyPhongGym.Domain.Entities.HoiVien>>
    {
        private readonly IApplicationDbContext _context;

        public GetHoiVienListQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<QuanLyPhongGym.Domain.Entities.HoiVien>> Handle(GetHoiVienListQuery request, CancellationToken cancellationToken)
        {
            // Lấy danh sách gồm các trường: MemberCode, FullName, Phone, Status 
            return await _context.HoiViens
                .AsNoTracking() // Tối ưu hiệu năng cho API theo đặc tả
                .ToListAsync(cancellationToken);
        }
    }
}