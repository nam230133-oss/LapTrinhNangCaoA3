using MediatR;
using Microsoft.AspNetCore.Http;

namespace QuanLyPhongGym.Application.Features.Auth.Commands.Register
{
    public class OtpRequest : IRequest<OtpResponse>
    {
        public string PhoneNumber { get; set; } = string.Empty;
    }
}