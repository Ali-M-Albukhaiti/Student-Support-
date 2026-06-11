using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentSupport.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedTestUser2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FullName", "IsAdmin", "Level", "PasswordHash", "Points", "Semester", "StudyProgram", "UserName" },
                values: new object[] { 2, "testuser2@student.fontys.nl", "Test User2", false, 1, "MTIzNA==", 0, 4, "Media", "testuser2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
