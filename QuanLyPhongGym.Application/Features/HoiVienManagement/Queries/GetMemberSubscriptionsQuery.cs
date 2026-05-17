using MediatR;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongGym.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QuanLyPhongGym.Application.Features.HoiVienManagement.Queries.GetMemberSubscriptions
{
    public record GetMemberSubscriptionsQuery(Guid HoiVienId) : IRequest<List<object>>;

    public class GetMemberSubscriptionsQueryHandler : IRequestHandler<GetMemberSubscriptionsQuery, List<object>>
    {
        private readonly IApplicationDbContext _context;

        public GetMemberSubscriptionsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<object>> Handle(GetMemberSubscriptionsQuery request, CancellationToken cancellationToken)
        {
            // 1. Tìm Họ và Tên của hội viên trước
            var memberName = await _context.HoiViens
                .Where(h => h.Id == request.HoiVienId)
                .Select(h => h.LastName + " " + h.FirstName) // Ghép Họ và Tên lại với nhau
                .FirstOrDefaultAsync(cancellationToken);

            // 2. Truy vấn danh sách thẻ và gắn Tên hội viên + Tên gói tập vào
            var subscriptions = await _context.TheHoiViens
                .Where(x => x.HoiVienId == request.HoiVienId)
                .Select(x => new
                {
                    SubscriptionId = x.Id,
                    MemberName = memberName, // Hiển thị Tên hội viên
                    PackageName = _context.GoiTaps
                                    .Where(g => g.Id == x.GoiTapId)
                                    .Select(g => g.TenGoi)
                                    .FirstOrDefault(), // Hiển thị Tên gói tập
                    StartDate = x.NgayKichHoat,
                    EndDate = x.NgayHetHan,
                    Status = x.TrangThai,
                    IsExpired = x.NgayHetHan < DateTime.Now
                })
                .ToListAsync(cancellationToken);

            // 3. Ép kiểu về List<object> để trả về
            return subscriptions.Cast<object>().ToList();
        }
    }
}