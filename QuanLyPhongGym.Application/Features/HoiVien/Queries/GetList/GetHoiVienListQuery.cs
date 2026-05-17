using MediatR;
using System.Collections.Generic;

namespace QuanLyPhongGym.Application.Features.HoiVienManagement.Queries.GetList
{
    // Sử dụng đường dẫn đầy đủ tới Domain để tránh lỗi "does not exist"
    public record GetHoiVienListQuery() : IRequest<List<QuanLyPhongGym.Domain.Entities.HoiVien>>;
}