using MediatR;

namespace QuanLyPhongGym.Application.Features.Products.Commands.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, decimal PurchasePrice, decimal SalePrice, int CurrentStock) : IRequest<bool>;
}