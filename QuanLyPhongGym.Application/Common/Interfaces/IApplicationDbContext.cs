// File: Common/Interfaces/IApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using QuanLyPhongGym.Domain.Entities;
using QuanLyPhongGym.Application.Common.Interfaces;

namespace QuanLyPhongGym.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Account> Accounts { get; }
        DbSet<HoiVien> HoiViens { get; }
        DbSet<GoiTap> GoiTaps { get; }
        DbSet<TheHoiVien> TheHoiViens { get; }
        DbSet<SanPham> SanPhams { get; }
        DbSet<CheckIn> CheckIns { get; }
        DbSet<LichTap> LichTaps { get; }
        DbSet<Contract> Contracts { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}