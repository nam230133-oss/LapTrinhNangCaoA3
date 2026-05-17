using System;

namespace QuanLyPhongGym.Domain.Entities
{
    public class TheHoiVien
    {
        public Guid Id { get; set; }

        // Khóa ngoại nối tới Hội viên [cite: 303, 451]
        public Guid HoiVienId { get; set; }

        // Khóa ngoại nối tới Gói tập [cite: 310]
        public Guid GoiTapId { get; set; }

        // Ngày bắt đầu sử dụng gói 
        public DateTime NgayKichHoat { get; set; }

        // Ngày hết hạn để hệ thống cảnh báo [cite: 75, 151]
        public DateTime NgayHetHan { get; set; }

        // Trạng thái thẻ: true (còn hạn), false (hết hạn/khóa) [cite: 77]
        public bool TrangThai { get; set; }
    }
}