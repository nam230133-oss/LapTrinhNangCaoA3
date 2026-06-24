namespace QuanLyPhongGym.Domain.Entities
{
    public class Contract // Bỏ chữ 'static' nếu có
    {
        public Guid Id { get; set; }
        public Guid HoiVienId { get; set; }
        public Guid GoiTapId { get; set; }
        public Guid NhanVienId { get; set; }
        public DateTime NgayTao { get; set; }

        // Navigation properties (nếu có)
        // public virtual Account NhanVien { get; set; }
    }
}