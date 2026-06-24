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

        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<HoiVien> HoiViens => Set<HoiVien>();
        public DbSet<GoiTap> GoiTaps => Set<GoiTap>();
        public DbSet<TheHoiVien> TheHoiViens => Set<TheHoiVien>();
        // Trong ApplicationDbContext.cs
        public DbSet<CheckIn> CheckIns => Set<CheckIn>();
        public DbSet<SanPham> SanPhams => Set<SanPham>();
        public DbSet<LichTap> LichTaps => Set<LichTap>();
        public DbSet<Contract> Contracts => Set<Contract>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --- Cấu hình mối quan hệ 1-1 giữa Account và HoiVien ---
            modelBuilder.Entity<Account>()
                .HasOne(a => a.HoiVien)              // Account có một HoiVien
                .WithOne(h => h.Account)             // HoiVien có một Account
                .HasForeignKey<Account>(a => a.HoiVienId) // Khóa ngoại nằm ở Account
                .IsRequired(false);                 // Cho phép Account không có HoiVien (tài khoản hệ thống)

            // --- 1. Cấu hình độ chính xác cho tiền tệ (GoiTap) ---
            modelBuilder.Entity<GoiTap>()
                .Property(g => g.Gia)
                .HasPrecision(18, 2);

            // --- 2. Cấu hình độ chính xác cho tiền tệ (SanPham) ---
            modelBuilder.Entity<SanPham>(entity =>
            {
                entity.Property(p => p.PurchasePrice).HasPrecision(18, 2);
                entity.Property(p => p.SalePrice).HasPrecision(18, 2);
            });
        }
    }
}