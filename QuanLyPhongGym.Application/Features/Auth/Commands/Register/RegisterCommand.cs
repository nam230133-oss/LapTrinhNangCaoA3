using MediatR;


namespace QuanLyPhongGym.Application.Features.Auth.Commands.Register
{
    public record RegisterCommand(
        string Phone,
        string Password,
        string ConfirmPassword,
        string Token2
    ) : IRequest<RegisterResponse>;

    public class RegisterResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}