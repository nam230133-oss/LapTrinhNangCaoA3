using MediatR;
using QuanLyPhongGym.Application.Common.Interfaces;

namespace QuanLyPhongGym.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        public UpdateProductCommandHandler(IApplicationDbContext context) => _context = context;

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.SanPhams.FindAsync(new object[] { request.Id }, cancellationToken);
            if (product == null) return false;

            product.Name = request.Name;
            product.PurchasePrice = request.PurchasePrice;
            product.SalePrice = request.SalePrice;
            product.CurrentStock = request.CurrentStock;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}