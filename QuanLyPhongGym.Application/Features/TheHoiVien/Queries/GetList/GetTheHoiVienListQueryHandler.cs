using MediatR;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongGym.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QuanLyPhongGym.Application.Features.TheHoiVien.Queries.GetList
{
    public class GetTheHoiVienListQueryHandler : IRequestHandler<GetTheHoiVienListQuery, List<TheHoiVienDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetTheHoiVienListQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TheHoiVienDto>> Handle(GetTheHoiVienListQuery request, CancellationToken cancellationToken)
        {
            // Sử dụng .Include để nạp dữ liệu từ các bảng liên quan và .Select để map sang DTO
            return await _context.TheHoiViens
                .Include(t => t.HoiVien)
                .Include(t => t.GoiTap)
                .Select(t => new TheHoiVienDto(
                    t.Id,
                    // Xử lý kiểm tra null an toàn cho Hội viên
                    t.HoiVien != null ? $"{t.HoiVien.FirstName} {t.HoiVien.LastName}" : "Hội viên không tồn tại",
                    t.HoiVien != null ? t.HoiVien.Phone ?? "Chưa cập nhật" : "N/A",
                    t.HoiVien != null ? t.HoiVien.Email ?? "Chưa cập nhật" : "N/A",
                    t.HoiVien != null ? t.HoiVien.Address ?? "Chưa cập nhật" : "N/A",
                    // Xử lý kiểm tra null an toàn cho Gói tập
                    t.GoiTap != null ? t.GoiTap.TenGoi ?? "Không rõ" : "Chưa chọn gói",
                    t.GoiTap != null ? t.GoiTap.Gia : 0,
                    t.GoiTap != null ? t.GoiTap.ThoiHanNgay : 0,
                    t.GoiTap != null ? t.GoiTap.MoTa ?? "" : "",
                    t.NgayKichHoat,
                    t.NgayHetHan,
                    t.TrangThai
                ))
                .ToListAsync(cancellationToken);
        }
    }
}