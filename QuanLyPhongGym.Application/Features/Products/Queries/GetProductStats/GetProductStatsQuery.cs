using MediatR;
using QuanLyPhongGym.Application.Features.Products.Queries.GetProductStats;

namespace QuanLyPhongGym.Application.Features.Products.Queries.GetProductStats;

// Record này đại diện cho yêu cầu lấy thống kê, trả về dữ liệu kiểu ProductStatsDto
public record GetProductStatsQuery : IRequest<ProductStatsDto>;