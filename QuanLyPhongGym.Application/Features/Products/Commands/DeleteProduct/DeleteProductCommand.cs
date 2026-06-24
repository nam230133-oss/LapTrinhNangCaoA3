using MediatR;

namespace QuanLyPhongGym.Application.Features.Products.Commands.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : IRequest<bool>;
}