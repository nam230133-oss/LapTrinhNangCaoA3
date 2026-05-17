using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyPhongGym.Domain.Entities;

namespace QuanLyPhongGym.Infrastructure.Persistence.Configurations
{
    public class HoiVienConfiguration : IEntityTypeConfiguration<HoiVien>
    {
        public void Configure(EntityTypeBuilder<HoiVien> builder)
        {
            // Tên bảng trong Database (theo Section 2.1.3 của tài liệu)
            builder.ToTable("Members");

            // Khóa chính
            builder.HasKey(x => x.Id);

            // Cấu hình Mã hội viên (MemberCode)
            builder.Property(x => x.MemberCode)
                .IsRequired()
                .HasMaxLength(20);

            // Cấu hình cột LastName và FirstName trong DB
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50); ;

            // Cấu hình Số điện thoại (Phone) - Thay thế cho 'SoDienThoai'
            builder.Property(x => x.Phone)
                .IsRequired()
                .HasMaxLength(15);

            // Cấu hình Email
            builder.Property(x => x.Email)
                .HasMaxLength(100);

            // Cấu hình dữ liệu khuôn mặt AI (FaceData)
            builder.Property(x => x.FaceData)
                .IsRequired(false);
        }
    }
}