using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using QuanLyPhongGym.Application.Common.Interfaces;
using QuanLyPhongGym.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QuanLyPhongGym.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMemoryCache _cache;

        public RegisterCommandHandler(IApplicationDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            // 1. Kiểm tra Token 2
            if (!_cache.TryGetValue(request.Token2, out string phone))
                throw new Exception("Token 2 hết hạn hoặc không hợp lệ!");

            // 2. Kiểm tra mật khẩu
            if (request.Password != request.ConfirmPassword)
                throw new Exception("Mật khẩu không khớp!");

            // 3. Tạo Account (Chỉ tạo Account cơ bản)
            var newAccount = new Account
            {
                Id = Guid.NewGuid(),
                Username = request.Phone,
                PhoneNumber = request.Phone,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password), // Lưu ý: Nên Hash mật khẩu ở đây
                CreatedDate = DateTime.UtcNow,
                Role = "Member"
            };

            _context.Accounts.Add(newAccount);
            await _context.SaveChangesAsync(cancellationToken);

            return new RegisterResponse { IsSuccess = true, Message = "Tạo tài khoản thành công!" };
        }
    }
}