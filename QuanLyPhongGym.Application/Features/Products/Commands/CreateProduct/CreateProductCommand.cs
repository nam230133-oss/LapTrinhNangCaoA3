using MediatR;
using System.Text.Json.Serialization;

namespace QuanLyPhongGym.Application.Features.Products.Commands.CreateProduct
{
    // Đặt DTO cùng cấp namespace để dễ dàng sử dụng trong Handler
    public record ProductCreatedDto(Guid Id, string Name);

    // Cập nhật IRequest trả về ProductCreatedDto thay vì Guid
    public record CreateProductCommand : IRequest<ProductCreatedDto>
    {
        public string SKU { get; init; }
        public string Name { get; init; }
        public string Category { get; init; }
        public string Unit { get; init; }
        public decimal PurchasePrice { get; init; }
        public decimal SalePrice { get; init; }
        public int InitialStock { get; init; }

        [JsonConstructor]
        public CreateProductCommand(string sku, string name, string category, string unit, decimal purchasePrice, decimal salePrice, int initialStock)
        {
            SKU = sku;
            Name = name;
            Category = category;
            Unit = unit;
            PurchasePrice = purchasePrice;
            SalePrice = salePrice;
            InitialStock = initialStock;
        }
    }
}