using MediatR;
namespace QuanLyPhongGym.Application.Features.LichTaps.Queries.GetLichTapByHoiVien;

public record GetLichTapByHoiVienQuery(Guid HoiVienId) : IRequest<List<LichTapDto>>;

public record LichTapDto(
    Guid Id,
    string PtName,
    DateTime ThoiGianBatDau,
    DateTime ThoiGianKetThuc,
    string TrangThai);