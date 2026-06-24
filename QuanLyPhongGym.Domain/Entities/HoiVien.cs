
namespace QuanLyPhongGym.Domain.Entities
{
    public class HoiVien
    {
        public Guid Id { get; set; }
        public string? MemberCode { get; set; }

        // Gán giá trị mặc định là chuỗi rỗng
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public Guid NhanVienTaoId { get; set; }
        public Guid? AccountId { get; set; }
        public Account? Account { get; set; }
        public string Status { get; set; } = "Active";
        public string? FaceData { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}