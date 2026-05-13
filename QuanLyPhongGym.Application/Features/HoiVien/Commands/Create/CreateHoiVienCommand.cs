// File: Features/HoiVien/Commands/Create/CreateHoiVienCommand.cs
using MediatR;

namespace QuanLyPhongGym.Application.Features.HoiVien.Commands.Create
{
    // Record đại diện cho dữ liệu client gửi lên. Trả về Guid (Id của hội viên mới)
    public record CreateHoiVienCommand(string Ten, string SoDienThoai) : IRequest<Guid>;
}