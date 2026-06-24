using System;
using System.Collections.Generic;
using System.Text;

namespace QuanLyPhongGym.Application.Features.Auth.Commands.Register
{
    public class VerifyOtpResponse
    {
        public string Message { get; set; } = string.Empty;
        public string Token2 { get; set; } = string.Empty;
    }
}