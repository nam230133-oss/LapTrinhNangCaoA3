using MediatR; // Đảm bảo đã có using này

namespace QuanLyPhongGym.Application.Features.Auth.Commands.Register
{
    public class OtpResponse
    {
        public string Message { get; set; } = string.Empty;
    }
}