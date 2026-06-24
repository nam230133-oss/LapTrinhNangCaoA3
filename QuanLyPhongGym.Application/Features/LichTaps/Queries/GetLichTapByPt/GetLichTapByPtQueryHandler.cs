using MediatR;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongGym.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QuanLyPhongGym.Application.Features.LichTaps.Queries.GetLichTapByPt
{
    // BẮT BUỘC phải có class để chứa phương thức
    public class GetLichTapByPtQueryHandler : IRequestHandler<GetLichTapByPtQuery, List<LichTapPtDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetLichTapByPtQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<LichTapPtDto>> Handle(GetLichTapByPtQuery request, CancellationToken cancellationToken)
        {
            return await _context.LichTaps
                .Where(l => l.HoiVienId == request.PtId)
                .Include(l => l.HoiVien)
                .Select(l => new LichTapPtDto(
                    l.Id,
                    l.HoiVien != null ? $"{l.HoiVien.LastName} {l.HoiVien.FirstName}" : "Chưa có hội viên", l.ThoiGianBatDau,
                    l.TrangThai))
                .ToListAsync(cancellationToken);
        }
    }
}