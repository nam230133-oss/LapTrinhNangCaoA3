using MediatR;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongGym.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QuanLyPhongGym.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<SanPhamDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllProductsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<SanPhamDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return await _context.SanPhams
                .Select(p => new SanPhamDto(
                    p.Id,
                    p.SKU,
                    p.Name,
                    p.Category,
                    p.Unit,
                    p.SalePrice,
                    p.CurrentStock
                ))
                .ToListAsync(cancellationToken);
        }
    }
}