using Microsoft.Extensions.Configuration;
using QuanLyPhongGym.Application.Common.Models;
using QuanLyPhongGym.Domain.Entities;
using QuanLyPhongGym.Infrastructure.Persistence.DbContext;
using System.Linq;
using System.Threading.Tasks;

public static class ApplicationDbContextSeed
{
    public static async Task SeedAsync(ApplicationDbContext context, IConfiguration configuration)
    {
        var adminSettings = configuration.GetSection("AdminSettings").Get<AdminSettings>();

        if (adminSettings != null && !context.Accounts.Any(a => a.Username == adminSettings.Username))
        {
            var admin = new Account
            {
                Id = Guid.NewGuid(), // Guid luôn cần giá trị
                Username = adminSettings.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(adminSettings.Password),
                Role = adminSettings.Role ?? "Admin", // Đảm bảo không null

                // String fields
                FirstName = "Admin",
                LastName = "System",
                Email = "admin@gym.com",
                PhoneNumber = "0000000000",
                Status = "Active",

                // DateTime field
                CreatedDate = DateTime.UtcNow, // Luôn dùng UtcNow cho chuẩn

                // Guid? (Nullable) -> An toàn
                StatusId = Guid.NewGuid(),
                HoiVienId = null
            };

            // BỔ SUNG: Kiểm tra xem còn thuộc tính nào trong class Account chưa được gán không
            // Nếu trong Account.cs bạn có: public Guid SomeId { get; set; } (không có ?)
            // Bạn PHẢI thêm: SomeId = Guid.Empty, vào đây

            context.Accounts.Add(admin);
            await context.SaveChangesAsync();
        }
    }
}