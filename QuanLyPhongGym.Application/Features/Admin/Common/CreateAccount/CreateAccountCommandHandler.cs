using MediatR;
using QuanLyPhongGym.Application.Common.Interfaces;
using QuanLyPhongGym.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuanLyPhongGym.Application.Features.Admin.Common.CreateAccount
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        public CreateAccountCommandHandler(IApplicationDbContext context) => _context = context;

        public async Task<Guid> Handle(CreateAccountCommand request, CancellationToken ct)
        {
            var account = new Account
            {
                Username = request.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,             // Kiểm tra xem đã gán chưa?
                PhoneNumber = request.PhoneNumber, // Kiểm tra xem đã gán chưa?
                Role = request.Role,
                Specialization = request.Specialization
            };
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync(ct);
            return account.Id;
        }
    }
}
