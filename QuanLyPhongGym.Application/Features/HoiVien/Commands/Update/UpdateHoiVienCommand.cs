using MediatR;
using System;

namespace QuanLyPhongGym.Application.Features.HoiVien.Commands.Update
{
    public record UpdateHoiVienCommand(
        Guid Id,
        string MemberCode,
        string LastName,
        string FirstName,
        string Phone,
        string Email,
        string Status) : IRequest<bool>;
}