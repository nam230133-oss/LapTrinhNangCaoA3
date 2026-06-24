using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace QuanLyPhongGym.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Đăng ký MediatR
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            // Nếu bạn có dùng FluentValidation, đăng ký ở đây luôn
            // services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMemoryCache();
            return services;
        }
    }
}