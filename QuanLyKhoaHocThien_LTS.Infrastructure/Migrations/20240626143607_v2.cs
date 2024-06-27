using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyKhoaHocThien_LTS.Infrastructure.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleCode", "RoleName" },
                values: new object[] { 1, "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleCode", "RoleName" },
                values: new object[] { 2, "User", "User" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
