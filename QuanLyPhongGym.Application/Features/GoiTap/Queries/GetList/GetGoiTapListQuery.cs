using MediatR;
using System.Collections.Generic;

namespace QuanLyPhongGym.Application.Features.GoiTap.Queries.GetList
{
    public record GetGoiTapListQuery : IRequest<List<GoiTapDto>>;

    public record GoiTapDto(Guid Id, string TenGoi, decimal Gia, int ThoiHanNgay);
}