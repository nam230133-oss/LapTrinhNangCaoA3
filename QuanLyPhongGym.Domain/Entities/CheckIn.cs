using System;
namespace QuanLyPhongGym.Domain.Entities
{
    public class CheckIn
    {
        public Guid Id { get; set; }
        public Guid HoiVienId { get; set; }
        public DateTime CheckInTime { get; set; }
        public virtual HoiVien HoiVien { get; set; } = null!;
    }
}