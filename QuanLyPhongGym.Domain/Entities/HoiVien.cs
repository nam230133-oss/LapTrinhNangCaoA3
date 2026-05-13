namespace QuanLyPhongGym.Domain.Entities
{
    public class HoiVien
    {
        public Guid Id { get; set; }
        public string Ten { get; set; } = string.Empty;
        public string SoDienThoai { get; set; } = string.Empty;
        public DateTime NgayThamGia { get; set; }
        public bool TrangThaiHoatDong { get; set; }
    }
}