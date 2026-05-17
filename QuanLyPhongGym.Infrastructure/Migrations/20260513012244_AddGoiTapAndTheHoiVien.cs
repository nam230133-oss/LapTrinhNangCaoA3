using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyPhongGym.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGoiTapAndTheHoiVien : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_HoiVien",
                table: "HoiVien");

            migrationBuilder.RenameTable(
                name: "HoiVien",
                newName: "HoiViens");

            migrationBuilder.AlterColumn<string>(
                name: "Ten",
                table: "HoiViens",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "SoDienThoai",
                table: "HoiViens",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AddPrimaryKey(
                name: "PK_HoiViens",
                table: "HoiViens",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "GoiTaps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenGoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gia = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ThoiHanNgay = table.Column<int>(type: "int", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoiTaps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TheHoiViens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HoiVienId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GoiTapId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NgayKichHoat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayHetHan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false),
                    MaVach_QRCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheHoiViens", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoiTaps");

            migrationBuilder.DropTable(
                name: "TheHoiViens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HoiViens",
                table: "HoiViens");

            migrationBuilder.RenameTable(
                name: "HoiViens",
                newName: "HoiVien");

            migrationBuilder.AlterColumn<string>(
                name: "Ten",
                table: "HoiVien",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "SoDienThoai",
                table: "HoiVien",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HoiVien",
                table: "HoiVien",
                column: "Id");
        }
    }
}
