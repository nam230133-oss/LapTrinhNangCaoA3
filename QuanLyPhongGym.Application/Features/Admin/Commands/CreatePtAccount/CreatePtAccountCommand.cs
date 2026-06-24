namespace QuanLyPhongGym.Application.Features.Admin.Commands.CreatePtAccount;

using MediatR;
using QuanLyPhongGym.Application.Features.Admin.Common;

public record CreatePtAccountCommand(
    string Username, string Password, string FirstName, string LastName,
    string Email, string PhoneNumber, string Specialization) : IRequest<Guid>;