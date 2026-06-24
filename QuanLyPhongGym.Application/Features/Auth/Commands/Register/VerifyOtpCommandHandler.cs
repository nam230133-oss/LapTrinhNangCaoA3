using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QuanLyPhongGym.Application.Features.Auth.Commands.Register
{
    // BẠN THIẾU CLASS Ở ĐÂY
    public class VerifyOtpCommandHandler : IRequestHandler<VerifyOtpCommand, VerifyOtpResponse>
    {
        private readonly IMemoryCache _cache;

        public VerifyOtpCommandHandler(IMemoryCache cache) => _cache = cache;

        public async Task<VerifyOtpResponse> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
        {
            if (!_cache.TryGetValue(request.PhoneNumber, out string savedOtp))
                throw new Exception("OTP đã hết hạn hoặc chưa yêu cầu OTP.");

            if (request.Otp != savedOtp)
                throw new Exception("Sai mã OTP!");

            var token2 = Guid.NewGuid().ToString();
            _cache.Set(token2, request.PhoneNumber, TimeSpan.FromMinutes(10));

            return new VerifyOtpResponse
            {
                Message = "Xác minh thành công!",
                Token2 = token2
            };
        }
    }
}