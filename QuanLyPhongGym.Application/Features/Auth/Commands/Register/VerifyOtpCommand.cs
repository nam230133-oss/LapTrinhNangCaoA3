using MediatR;

namespace QuanLyPhongGym.Application.Features.Auth.Commands.Register
{
    // Cần khớp với IRequest<VerifyOtpResponse>
    public record VerifyOtpCommand(string PhoneNumber, string Otp) : IRequest<VerifyOtpResponse>;
}