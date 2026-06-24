using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http; // Bắt buộc cho IHttpContextAccessor
using QuanLyPhongGym.Application.Common.Interfaces;
using QuanLyPhongGym.Infrastructure.Persistence.DbContext;
using QuanLyPhongGym.Infrastructure.Services; // Đảm bảo namespace này khớp với file CurrentUserService.cs

namespace QuanLyPhongGym.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // 1. Đăng ký DbContext với SQL Server
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            // 2. Đăng ký IApplicationDbContext để tầng Application sử dụng
            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

            // 3. Đăng ký các dịch vụ hệ thống
            services.AddHttpContextAccessor(); // Cần thiết để lấy thông tin từ Token
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            return services;
        }
    }
}