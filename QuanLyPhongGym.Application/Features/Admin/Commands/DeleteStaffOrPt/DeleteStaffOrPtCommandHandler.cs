using MediatR;
using QuanLyPhongGym.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuanLyPhongGym.Application.Features.Admin.Commands.DeleteStaffOrPt
{
    public class DeleteStaffOrPtCommandHandler : IRequestHandler<DeleteStaffOrPtCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        public DeleteStaffOrPtCommandHandler(IApplicationDbContext context) => _context = context;

        public async Task<bool> Handle(DeleteStaffOrPtCommand request, CancellationToken ct)
        {
            // Lấy danh sách tài khoản cần xóa
            var accounts = await _context.Accounts
                .Where(a => request.Ids.Contains(a.Id))
                .ToListAsync(ct);

            if (!accounts.Any()) return false;

            // Xóa hàng loạt
            _context.Accounts.RemoveRange(accounts);
            await _context.SaveChangesAsync(ct);

            return true;
        }
    }
}
