using MediatR;
using QuanLyPhongGym.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace QuanLyPhongGym.Application.Features.Products.Commands.SellProduct
{
    // Đảm bảo SellProductCommand cũng được cập nhật IRequest<SellProductResult>
    // public record SellProductCommand(Guid ProductId, int Quantity) : IRequest<SellProductResult>;

    public class SellProductCommandHandler : IRequestHandler<SellProductCommand, SellProductResult>
    {
        private readonly IApplicationDbContext _context;

        public SellProductCommandHandler(IApplicationDbContext context) => _context = context;

        public async Task<SellProductResult> Handle(SellProductCommand request, CancellationToken ct)
        {
            var product = await _context.SanPhams.FindAsync(request.ProductId);

            if (product == null || product.CurrentStock < request.Quantity)
                return new SellProductResult(false, string.Empty, 0);

            product.CurrentStock -= request.Quantity;
            await _context.SaveChangesAsync(ct);

            return new SellProductResult(true, product.Name, request.Quantity);
        }
    }

    // Đặt record này bên ngoài class hoặc trong file riêng nếu cần dùng ở Controller
    public record SellProductResult(bool Success, string ProductName, int Quantity);
}