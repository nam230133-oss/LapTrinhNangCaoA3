// File: Persistence/DbContext/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using QuanLyPhongGym.Application.Common.Interfaces;
using QuanLyPhongGym.Domain.Entities;
using System.Reflection;
using System.Reflection.Emit;

namespace QuanLyPhongGym.Infrastructure.Persistence.DbContext
{
    // Kế thừa từ DbContext của EF Core và implement interface của tầng Application
    public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<HoiVien> HoiViens { get; set; }
        public DbSet<GoiTap> GoiTaps { get; set; }
        public DbSet<TheHoiVien> TheHoiViens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình cho thuộc tính Gia của GoiTap
            modelBuilder.Entity<GoiTap>()
                .Property(g => g.Gia)
                .HasPrecision(18, 2); // 18 chữ số, 2 số sau dấu phẩy
        }
    }
}