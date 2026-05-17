using MediatR;

namespace QuanLyPhongGym.Application.Features.HoiVien.Commands.Create
{
    public record CreateHoiVienCommand(
        string MemberCode,
        string LastName,
        string FirstName,
        string Phone,
        string? Email,
        string? Gender) : IRequest<Guid>;

}