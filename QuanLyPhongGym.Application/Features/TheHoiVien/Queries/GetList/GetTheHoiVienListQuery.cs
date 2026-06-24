using MediatR;
using System.Collections.Generic;

namespace QuanLyPhongGym.Application.Features.TheHoiVien.Queries.GetList
{
    // Trong file chứa TheHoiVienDto
    public record TheHoiVienDto(
        Guid Id,
        // Thông tin Hội viên
        string HoTen,
        string SoDienThoai,
        string Email,
        string DiaChi,
        // Thông tin Gói tập
        string TenGoiTap,
        decimal Gia,
        int ThoiHanNgay,
        string MoTa,
        // Thông tin Thẻ
        DateTime NgayKichHoat,
        DateTime NgayHetHan,
        bool TrangThai
    );
    public record GetTheHoiVienListQuery : IRequest<List<TheHoiVienDto>>;
}