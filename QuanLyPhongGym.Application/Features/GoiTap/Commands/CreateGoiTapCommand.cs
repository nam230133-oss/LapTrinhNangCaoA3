using MediatR;
using QuanLyPhongGym.Application.Common.Interfaces;
using QuanLyPhongGym.Domain.Entities;

namespace QuanLyPhongGym.Application.Features.GoiTaps.Commands
{
    public record CreateGoiTapCommand(
        string TenGoi,
        int ThoiHanNgay,
        decimal Gia,
        string? MoTa,
        int? SoBuoi) : IRequest<Guid>;
}
