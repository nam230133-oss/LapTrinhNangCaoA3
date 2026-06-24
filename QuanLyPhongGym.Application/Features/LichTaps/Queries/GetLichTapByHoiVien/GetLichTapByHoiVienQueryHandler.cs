using MediatR;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongGym.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace QuanLyPhongGym.Application.Features.LichTaps.Queries.GetLichTapByHoiVien
{
    public class GetLichTapByHoiVienQueryHandler : IRequestHandler<GetLichTapByHoiVienQuery, List<LichTapDto>>
    {
        private readonly IApplicationDbContext _context;
        public GetLichTapByHoiVienQueryHandler(IApplicationDbContext context) => _context = context;

        public async Task<List<LichTapDto>> Handle(GetLichTapByHoiVienQuery request, CancellationToken cancellationToken)
        {
            return await _context.LichTaps
                .Where(l => l.HoiVienId == request.HoiVienId)
                .Select(l => new LichTapDto(
                    l.Id,
                    "Chưa xác định",
                    l.ThoiGianBatDau,
                    l.ThoiGianKetThuc,
                    l.TrangThai
))
                .ToListAsync(cancellationToken);
        }
    }
}
