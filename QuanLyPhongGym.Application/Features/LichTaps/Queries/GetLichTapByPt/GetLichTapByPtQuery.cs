using MediatR;
namespace QuanLyPhongGym.Application.Features.LichTaps.Queries.GetLichTapByPt;

public record GetLichTapByPtQuery(Guid PtId) : IRequest<List<LichTapPtDto>>;

public record LichTapPtDto(
    Guid Id,
    string HoiVienName,
    DateTime ThoiGianBatDau,
    string TrangThai);