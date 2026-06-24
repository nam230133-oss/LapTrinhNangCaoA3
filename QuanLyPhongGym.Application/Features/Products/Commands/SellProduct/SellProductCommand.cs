using MediatR;
using System;
using static QuanLyPhongGym.Application.Features.Products.Commands.SellProduct.SellProductCommandHandler;

namespace QuanLyPhongGym.Application.Features.Products.Commands.SellProduct
{
    // Đảm bảo bạn đã thêm int Quantity vào đây
    // Mở file SellProductCommand.cs
    public record SellProductCommand(Guid ProductId, int Quantity) : IRequest<SellProductResult>;
}