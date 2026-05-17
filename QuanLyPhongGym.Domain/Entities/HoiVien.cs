using System;

namespace QuanLyPhongGym.Domain.Entities
{
    public class HoiVien
    {
        public Guid Id { get; set; }
        public string MemberCode { get; set; } = string.Empty; // Mã hội viên 
        public string LastName { get; set; } = string.Empty;  // Họ và tên đệm (VD: Nguyễn Lê)
        public string FirstName { get; set; } = string.Empty; // Tên chính (VD: Minh)
        public string Phone { get; set; } = string.Empty;      // Số điện thoại 
        public string? Email { get; set; }                     // Email 
        public string? FaceData { get; set; }                  // Dữ liệu khuôn mặt AI 
        public DateTime CreatedDate { get; set; }              // Ngày tạo 
        public bool Status { get; set; }                       // Trạng thái tài khoản 
    }
}