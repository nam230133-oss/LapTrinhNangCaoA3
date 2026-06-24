using MediatR;
using BCrypt.Net;
namespace QuanLyPhongGym.Application.Features.Admin.Common;

public record CreateAccountCommand(
    string Username,
    string Password,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Role,
    string? Specialization = null // Mặc định là null
) : IRequest<Guid>;