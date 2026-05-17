// File: Common/Interfaces/IApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using QuanLyPhongGym.Domain.Entities;

namespace QuanLyPhongGym.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<HoiVien> HoiViens { get; set; }
        DbSet<GoiTap> GoiTaps { get; set; }        // Thêm dòng này
        DbSet<TheHoiVien> TheHoiViens { get; set; } // Thêm dòng này

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}