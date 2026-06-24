using System;

namespace QuanLyPhongGym.Domain.Entities
{
    public class Account
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty; // Admin, Staff, PT

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? TempField { get; set; }

        // Hội viên 
        public Guid? HoiVienId { get; set; }
        public HoiVien? HoiVien { get; set; }
        // Riêng cho PT
        public string? Specialization { get; set; }

        // Trường quản lý hệ thống
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public Guid StatusId { get; set; }
        public string Status { get; set; } = "Active";
    }
}