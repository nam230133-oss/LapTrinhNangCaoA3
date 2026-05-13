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

        public DbSet<HoiVien> HoiViens => Set<HoiVien>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Tự động quét và apply cái HoiVienConfiguration ở trên
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}