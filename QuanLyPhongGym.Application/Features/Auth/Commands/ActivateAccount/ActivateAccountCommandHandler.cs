using MediatR;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongGym.Application.Common.Interfaces;
using QuanLyPhongGym.Domain.Entities;
using Microsoft.AspNetCore.Identity; // Cần cài gói Microsoft.Extensions.Identity.Core
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QuanLyPhongGym.Application.Features.Auth.Commands.ActivateAccount
{
    // BẮT BUỘC PHẢI CÓ CLASS NÀY BAO QUANH
    public class ActivateAccountCommandHandler : IRequestHandler<ActivateAccountCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        private readonly IPasswordHasher<Account> _passwordHasher;

        // Constructor để tiêm (Inject) các service vào
        public ActivateAccountCommandHandler(IApplicationDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<Account>();
        }

        public async Task<bool> Handle(ActivateAccountCommand request, CancellationToken ct)
        {
            // 1. Tìm Hội viên
            var hoiVien = await _context.HoiViens.FirstOrDefaultAsync(h => h.Phone == request.Phone, ct);
            if (hoiVien == null) throw new Exception("Hội viên không tồn tại.");

            // 2. Tạo tài khoản mới
            var account = new Account
            {
                Username = request.Phone,
                PasswordHash = _passwordHasher.HashPassword(null, request.Password),
                HoiVienId = hoiVien.Id,
                // Các thuộc tính khác nếu có trong entity Account của bạn
            };

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync(ct);
            return true;
        }
    }
}