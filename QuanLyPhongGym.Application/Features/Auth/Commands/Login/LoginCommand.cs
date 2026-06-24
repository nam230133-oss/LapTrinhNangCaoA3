using MediatR;

namespace QuanLyPhongGym.Application.Features.Auth.Commands.Login
{
    // Record định nghĩa dữ liệu Postman/Frontend gửi lên và kiểu trả về là LoginResponse
    public record LoginCommand(string Username, string Password) : IRequest<LoginResponse>;

    // Class chứa dữ liệu trả về cho client
    public class LoginResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty; // Admin, Member, Staff, PT...
    }
}