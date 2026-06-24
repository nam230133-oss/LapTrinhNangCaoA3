using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using QuanLyPhongGym.Application.Common.Interfaces;
using QuanLyPhongGym.Infrastructure;
using QuanLyPhongGym.Infrastructure.Persistence.DbContext;
using System.Security.Claims; // Bắt buộc cho ClaimTypes
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// --- 1. CẤU HÌNH SERVICES ---
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           .ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning))); // Thêm dòng này vào

builder.Services.AddScoped<IApplicationDbContext>(provider =>
    provider.GetRequiredService<ApplicationDbContext>());

builder.Services.AddMemoryCache();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(QuanLyPhongGym.Application.DependencyInjection).Assembly);
});

// Cấu hình Authentication & Authorization
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"] ?? "Sieu_Mat_Khau_Bao_Mat_Phong_Gym_Tich_Hop_AI_2026")),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,

        NameClaimType = ClaimTypes.Name,
        RoleClaimType = ClaimTypes.Role // Khớp với Key trong Token
    };

    // Event để map "role" sang ClaimTypes.Role, giúp [Authorize(Roles=...)] hoạt động
    options.Events = new JwtBearerEvents
    {
        OnTokenValidated = context =>
        {
            var identity = context.Principal?.Identity as ClaimsIdentity;
            // Ánh xạ claim "role" (key trong token) sang ClaimTypes.Role (hệ thống hiểu)
            var roleClaim = identity?.FindFirst("role");
            if (roleClaim != null)
            {
                identity?.AddClaim(new Claim(ClaimTypes.Role, roleClaim.Value));
            }
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization(); // BẮT BUỘC để dùng [Authorize]

// 1. Cấu hình Controller
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// 2. Cấu hình Authorization tách riêng
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("StaffOrAdmin", policy => policy.RequireRole("Admin", "Staff"));
});

var app = builder.Build();

// --- 2. MIDDLEWARE PIPELINE (Thứ tự rất quan trọng) ---
app.UseHttpsRedirection();

app.UseAuthentication(); // 1. Xác thực (Ai đang vào?)
app.UseAuthorization();  // 2. Phân quyền (Có quyền làm việc này không?)

app.MapControllers();

// Seeding Admin
// Sửa phần Seeding trong Program.cs
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        // Chỉ Migrate khi DB chưa tồn tại hoặc cần update
        await context.Database.MigrateAsync();

        var configuration = services.GetRequiredService<IConfiguration>();
        await ApplicationDbContextSeed.SeedAsync(context, configuration);
    }
    catch (Exception ex)
    {
        // Ghi log lỗi ra console để biết chính xác lỗi ở đâu
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Lỗi khi Migrate hoặc Seed dữ liệu.");
        throw; // Ném lại lỗi để dừng ứng dụng nếu không thể chạy được
    }
}

app.Run();