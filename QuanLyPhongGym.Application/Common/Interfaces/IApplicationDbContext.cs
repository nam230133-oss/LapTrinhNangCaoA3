// File: Common/Interfaces/IApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using QuanLyPhongGym.Domain.Entities;

namespace QuanLyPhongGym.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<HoiVien> HoiViens { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}