using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentSupport.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedTestUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FullName", "IsAdmin", "Level", "PasswordHash", "Points", "Semester", "StudyProgram", "UserName" },
                values: new object[] { 1, "testuser@student.fontys.nl", "Test User", false, 1, "MTIzNA==", 0, 3, "ICT", "testuser" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
