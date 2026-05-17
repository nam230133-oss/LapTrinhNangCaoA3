using MediatR;

namespace QuanLyPhongGym.Application.Features.HoiVienManagement.Queries.GetWithPaginatedList
{
    // Định nghĩa các tham số phân trang và tìm kiếm nhận từ client
    public record GetHoiVienPaginatedQuery(
        int PageNumber = 1,
        int PageSize = 10,
        string? SearchTerm = null) : IRequest<object>;
}