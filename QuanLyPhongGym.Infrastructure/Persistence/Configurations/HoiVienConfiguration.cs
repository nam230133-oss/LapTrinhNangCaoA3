// File: Persistence/Configurations/HoiVienConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyPhongGym.Domain.Entities;

namespace QuanLyPhongGym.Infrastructure.Persistence.Configurations
{
    public class HoiVienConfiguration : IEntityTypeConfiguration<HoiVien>
    {
        public void Configure(EntityTypeBuilder<HoiVien> builder)
        {
            builder.ToTable("HoiVien");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Ten).IsRequired().HasMaxLength(100);
            builder.Property(x => x.SoDienThoai).HasMaxLength(15);
        }
    }
}