using MediatR;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongGym.Application.Common.Interfaces;

namespace QuanLyPhongGym.Application.Features.Products.Queries.GetProductStats
{
    public class GetProductStatsQueryHandler : IRequestHandler<GetProductStatsQuery, ProductStatsDto>
    {
        private readonly IApplicationDbContext _context;

        public GetProductStatsQueryHandler(IApplicationDbContext context) => _context = context;

        public async Task<ProductStatsDto> Handle(GetProductStatsQuery request, CancellationToken cancellationToken)
        {
            var products = await _context.SanPhams.ToListAsync(cancellationToken);

            return new ProductStatsDto(
                TotalSKU: products.Count,
                TotalStock: products.Sum(p => p.CurrentStock),
                EstimatedValue: products.Sum(p => p.CurrentStock * p.PurchasePrice),
                LowStockCount: products.Count(p => p.CurrentStock <= 5)
            );
        }
    }
}