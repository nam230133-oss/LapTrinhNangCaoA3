namespace QuanLyPhongGym.Application.Features.Products.Queries.GetProductStats;

public record ProductStatsDto(
    int TotalSKU,
    int TotalStock,
    decimal EstimatedValue,
    int LowStockCount
);