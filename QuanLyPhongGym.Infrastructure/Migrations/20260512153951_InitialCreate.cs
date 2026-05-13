using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyPhongGym.Infrastructure.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "HoiVien",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Ten = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                SoDienThoai = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                NgayThamGia = table.Column<DateTime>(type: "datetime2", nullable: false),
                TrangThaiHoatDong = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_HoiVien", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "HoiVien");
    }
}
