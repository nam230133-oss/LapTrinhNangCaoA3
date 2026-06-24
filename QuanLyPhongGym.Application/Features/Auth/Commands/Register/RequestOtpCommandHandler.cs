using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using QuanLyPhongGym.Application.Common.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QuanLyPhongGym.Application.Features.Auth.Commands.Register
{
    // 1. Sửa từ <OtpRequest, string> thành <OtpRequest, OtpResponse>
    public class RequestOtpCommandHandler : IRequestHandler<OtpRequest, OtpResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMemoryCache _cache;

        public RequestOtpCommandHandler(IApplicationDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        // 2. Sửa kiểu trả về của Task từ string thành OtpResponse
        public async Task<OtpResponse> Handle(OtpRequest request, CancellationToken cancellationToken)
        {
            if (await _context.HoiViens.AnyAsync(h => h.Phone == request.PhoneNumber, cancellationToken))
                throw new Exception("SĐT đã được đăng ký!");

            string otp = new Random().Next(0, 1000000).ToString("D6");

            // Lưu vào cache với khóa là số điện thoại
            _cache.Set(request.PhoneNumber, otp, TimeSpan.FromMinutes(5));

            return new OtpResponse
            {
                Message = $"Mã OTP đã được gửi đến số điện thoại của bạn! Mã OTP là: {otp}"
            };
        }
    }
}