using MediatR;
using QuanLyPhongGym.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuanLyPhongGym.Application.Features.Admin.Queries
{
    public class GetStaffAndPtListQueryHandler : IRequestHandler<GetStaffAndPtListQuery, List<StaffAndPtDto>>
    {
        private readonly IApplicationDbContext _context;
        public GetStaffAndPtListQueryHandler(IApplicationDbContext context) => _context = context;

        public async Task<List<StaffAndPtDto>> Handle(GetStaffAndPtListQuery request, CancellationToken ct)
        {
            return await _context.Accounts
                .Where(a => a.Role == "PT" || a.Role == "Staff")
                .Select(a => new StaffAndPtDto
                {
                    Id = a.Id,
                    Username = a.Username,
                    FullName = a.FirstName + " " + a.LastName,
                    Email = a.Email,
                    PhoneNumber = a.PhoneNumber,
                    Role = a.Role,
                    Specialization = a.Specialization
                })
                .ToListAsync(ct);
        }
    }
}
