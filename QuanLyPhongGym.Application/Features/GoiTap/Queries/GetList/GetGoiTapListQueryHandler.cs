using MediatR;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongGym.Application.Common.Interfaces;

namespace QuanLyPhongGym.Application.Features.GoiTap.Queries.GetList
{
    public class GetGoiTapListQueryHandler : IRequestHandler<GetGoiTapListQuery, List<GoiTapDto>>
    {
        private readonly IApplicationDbContext _context;
        public GetGoiTapListQueryHandler(IApplicationDbContext context) => _context = context;

        public async Task<List<GoiTapDto>> Handle(GetGoiTapListQuery request, CancellationToken cancellationToken)
        {
            return await _context.GoiTaps
                .Select(g => new GoiTapDto(g.Id, g.TenGoi, g.Gia, g.ThoiHanNgay))
                .ToListAsync(cancellationToken);
        }
    }
}