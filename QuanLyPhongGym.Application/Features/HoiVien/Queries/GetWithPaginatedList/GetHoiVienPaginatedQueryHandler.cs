using MediatR;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongGym.Application.Common.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QuanLyPhongGym.Application.Features.HoiVienManagement.Queries.GetWithPaginatedList
{
    public class GetHoiVienPaginatedQueryHandler : IRequestHandler<GetHoiVienPaginatedQuery, object>
    {
        private readonly IApplicationDbContext _context;

        public GetHoiVienPaginatedQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<object> Handle(GetHoiVienPaginatedQuery request, CancellationToken cancellationToken)
        {
            var query = _context.HoiViens.AsQueryable();

            // 1. Lọc dữ liệu theo từ khóa tìm kiếm (Tên hoặc Số điện thoại) nếu có
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                query = query.Where(h => h.FirstName.Contains(request.SearchTerm) || h.Phone.Contains(request.SearchTerm));
            }

            // 2. Tính toán tổng số lượng bản ghi để chia trang
            var totalRecords = await query.CountAsync(cancellationToken);
            var totalPages = (int)Math.Ceiling((double)totalRecords / request.PageSize);

            // 3. Thực hiện phân trang dưới Database bằng Skip và Take
            var items = await query
                .OrderByDescending(h => h.CreatedDate) // Ưu tiên xếp hội viên mới lên đầu
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(h => new
                {
                    h.Id,
                    h.MemberCode,
                    FullName = h.LastName + " " + h.FirstName,
                    h.Phone,
                    h.Status
                })
                .ToListAsync(cancellationToken);

            // 4. Trả về cấu trúc gói dữ liệu hoàn chỉnh cho giao diện dễ xử lý nút bấm trang
            return new
            {
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                CurrentPage = request.PageNumber,
                PageSize = request.PageSize,
                Data = items
            };
        }
    }
}