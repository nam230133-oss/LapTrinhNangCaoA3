using MediatR;

namespace QuanLyPhongGym.Application.Features.Admin.Commands.CreateStaffAccount;

public record CreateStaffAccountCommand(
    string Username,
    string Password,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber
    ) : IRequest<Guid>;