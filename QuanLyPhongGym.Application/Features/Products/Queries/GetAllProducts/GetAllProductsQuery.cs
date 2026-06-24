using MediatR;
using System;
using System.Collections.Generic;

namespace QuanLyPhongGym.Application.Features.Products.Queries.GetAllProducts
{
    // DTO để trả về dữ liệu sạch cho Frontend
    public record SanPhamDto(Guid Id, string SKU, string Name, string Category, string Unit, decimal SalePrice, int CurrentStock);

    // Định nghĩa Query trả về một List các SanPhamDto
    public record GetAllProductsQuery : IRequest<List<SanPhamDto>>;
}