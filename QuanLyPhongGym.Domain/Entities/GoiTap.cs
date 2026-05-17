namespace QuanLyPhongGym.Domain.Entities
{
    public class GoiTap
    {
        public Guid Id { get; set; }
        public string TenGoi { get; set; } = string.Empty; // Tên gói tập 
        public int ThoiHanNgay { get; set; } // Thời hạn (số ngày) 
        public int? SoBuoi { get; set; } // Số buổi (nếu là gói PT hoặc gói theo buổi) 
        public decimal Gia { get; set; } // Giá tiền 
        public string? MoTa { get; set; } // Mô tả chi tiết 
        public bool TrangThai { get; set; } = true; // Trạng thái gói (Đang bán/Ngừng bán) 
    }
}