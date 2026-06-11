using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentSupport.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeededUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "MTIzNDU=");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "MTIzNA==");
        }
    }
}
