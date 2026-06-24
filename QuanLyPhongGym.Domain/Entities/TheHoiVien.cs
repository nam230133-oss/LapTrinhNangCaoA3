namespace QuanLyPhongGym.Domain.Entities
{
    public class TheHoiVien
    {
        public Guid Id { get; set; }
        public Guid HoiVienId { get; set; }

        // Bắt buộc phải có thuộc tính này để dùng .Include(t => t.HoiVien)
        public HoiVien? HoiVien { get; set; }
        public GoiTap? GoiTap { get; set; }
        public Guid GoiTapId { get; set; }
        public DateTime NgayKichHoat { get; set; }
        public DateTime NgayHetHan { get; set; }
        public bool TrangThai { get; set; }
    }
}