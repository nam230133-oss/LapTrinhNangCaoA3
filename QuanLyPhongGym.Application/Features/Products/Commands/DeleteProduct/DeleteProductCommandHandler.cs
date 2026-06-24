using MediatR;
using QuanLyPhongGym.Application.Common.Interfaces;

namespace QuanLyPhongGym.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        public DeleteProductCommandHandler(IApplicationDbContext context) => _context = context;

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.SanPhams.FindAsync(new object[] { request.Id }, cancellationToken);
            if (product == null) return false;

            _context.SanPhams.Remove(product);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}