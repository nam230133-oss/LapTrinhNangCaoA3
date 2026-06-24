using MediatR;
using QuanLyPhongGym.Application.Common.Interfaces;
using QuanLyPhongGym.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace QuanLyPhongGym.Application.Features.Products.Commands.CreateProduct
{
    // ĐỔI TỪ Guid SANG ProductCreatedDto Ở ĐÂY
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductCreatedDto>
    {
        private readonly IApplicationDbContext _context;
        public CreateProductCommandHandler(IApplicationDbContext context) => _context = context;

        // ĐỔI KIỂU TRẢ VỀ CỦA HÀM Handle
        public async Task<ProductCreatedDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new SanPham
            {
                Id = Guid.NewGuid(),
                SKU = request.SKU,
                Name = request.Name,
                Category = request.Category,
                Unit = request.Unit,
                PurchasePrice = request.PurchasePrice,
                SalePrice = request.SalePrice,
                CurrentStock = request.InitialStock
            };

            _context.SanPhams.Add(product);
            await _context.SaveChangesAsync(cancellationToken);

            // Trả về DTO thay vì Guid
            return new ProductCreatedDto(product.Id, product.Name);
        }
    }
}