using MediatR;

namespace QuanLyPhongGym.Application.Features.LichTaps.Commands.CreateLichTap;

public record CreateLichTapCommand(
    Guid PtId,
    Guid HoiVienId,
    DateTime ThoiGianBatDau,
    DateTime ThoiGianKetThuc) : IRequest<Guid>;