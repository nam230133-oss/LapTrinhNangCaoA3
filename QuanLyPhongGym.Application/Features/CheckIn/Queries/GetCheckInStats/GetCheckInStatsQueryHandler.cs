using MediatR;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongGym.Application.Common.Interfaces; // Quan trọng: chứa IApplicationDbContext
using System.Threading;
using System.Threading.Tasks;
using System;

namespace QuanLyPhongGym.Application.Features.CheckIn.Queries.GetCheckInStats
{
    // 1. Phải khai báo lớp handler kế thừa IRequestHandler
    public class GetCheckInStatsQueryHandler : IRequestHandler<GetCheckInStatsQuery, int>
    {
        private readonly IApplicationDbContext _context;

        // 2. Phải có Constructor để tiêm _context vào
        public GetCheckInStatsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        // 3. Phương thức Handle nằm trong class
        public async Task<int> Handle(GetCheckInStatsQuery request, CancellationToken cancellationToken)
        {
            var today = DateTime.UtcNow.Date;
            return await _context.CheckIns
                .CountAsync(c => c.CheckInTime.Date == today, cancellationToken);
        }
    }
}