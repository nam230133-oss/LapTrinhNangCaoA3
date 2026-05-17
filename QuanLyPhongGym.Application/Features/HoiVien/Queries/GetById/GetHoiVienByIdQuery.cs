using MediatR;
using System;

namespace QuanLyPhongGym.Application.Features.HoiVienManagement.Queries.GetById
{
    // Định nghĩa tham số đầu vào cho yêu cầu tìm kiếm theo Id
    public record GetHoiVienByIdQuery(Guid Id) : IRequest<object?>;
}