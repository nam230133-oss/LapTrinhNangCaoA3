using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyPhongGym.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeHoiVienIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_HoiViens_HoiVienId",
                table: "Accounts");

            migrationBuilder.AlterColumn<Guid>(
                name: "HoiVienId",
                table: "Accounts",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_HoiViens_HoiVienId",
                table: "Accounts",
                column: "HoiVienId",
                principalTable: "HoiViens",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_HoiViens_HoiVienId",
                table: "Accounts");

            migrationBuilder.AlterColumn<Guid>(
                name: "HoiVienId",
                table: "Accounts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_HoiViens_HoiVienId",
                table: "Accounts",
                column: "HoiVienId",
                principalTable: "HoiViens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
