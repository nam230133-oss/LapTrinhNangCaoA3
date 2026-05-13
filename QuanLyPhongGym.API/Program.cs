using Microsoft.EntityFrameworkCore;
using QuanLyPhongGym.Application.Common.Interfaces;
using QuanLyPhongGym.Infrastructure.Persistence.DbContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// ================= BẮT ĐẦU THÊM CẤU HÌNH TẠI ĐÂY =================

// 1. Đăng ký ApplicationDbContext với SQL Server (Lấy chuỗi kết nối từ appsettings.json)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Đăng ký interface IApplicationDbContext map với ApplicationDbContext
builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

// 3. Đăng ký MediatR (Quét tầng Application để tìm các Handler như CreateHoiVienCommandHandler)
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(QuanLyPhongGym.Application.Features.HoiVien.Commands.Create.CreateHoiVienCommand).Assembly);
});

// ================= KẾT THÚC THÊM CẤU HÌNH =================

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();