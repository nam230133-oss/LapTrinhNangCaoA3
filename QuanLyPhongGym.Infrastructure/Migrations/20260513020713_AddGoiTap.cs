using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyPhongGym.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGoiTap : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaVach_QRCode",
                table: "TheHoiViens");

            migrationBuilder.RenameColumn(
                name: "TrangThaiHoatDong",
                table: "HoiViens",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "Ten",
                table: "HoiViens",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "SoDienThoai",
                table: "HoiViens",
                newName: "MemberCode");

            migrationBuilder.RenameColumn(
                name: "NgayThamGia",
                table: "HoiViens",
                newName: "CreatedDate");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "HoiViens",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FaceData",
                table: "HoiViens",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "HoiViens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "TenGoi",
                table: "GoiTaps",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SoBuoi",
                table: "GoiTaps",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TrangThai",
                table: "GoiTaps",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "HoiViens");

            migrationBuilder.DropColumn(
                name: "FaceData",
                table: "HoiViens");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "HoiViens");

            migrationBuilder.DropColumn(
                name: "SoBuoi",
                table: "GoiTaps");

            migrationBuilder.DropColumn(
                name: "TrangThai",
                table: "GoiTaps");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "HoiViens",
                newName: "TrangThaiHoatDong");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "HoiViens",
                newName: "Ten");

            migrationBuilder.RenameColumn(
                name: "MemberCode",
                table: "HoiViens",
                newName: "SoDienThoai");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "HoiViens",
                newName: "NgayThamGia");

            migrationBuilder.AddColumn<string>(
                name: "MaVach_QRCode",
                table: "TheHoiViens",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TenGoi",
                table: "GoiTaps",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
