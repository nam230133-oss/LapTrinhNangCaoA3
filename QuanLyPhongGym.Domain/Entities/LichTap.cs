using QuanLyPhongGym.Domain.Entities;

public class LichTap
{
    public Guid Id { get; set; }
    public Guid HoiVienId { get; set; }
    // Navigation properties
    public virtual HoiVien? HoiVien { get; set; }

    public DateTime ThoiGianBatDau { get; set; }
    public DateTime ThoiGianKetThuc { get; set; }
    public string TrangThai { get; set; } = "Pending";
}