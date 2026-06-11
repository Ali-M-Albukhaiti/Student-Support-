using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentSupport.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedPosts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorId", "Content", "CreatedAt", "Semester", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, "Just completed semester 3 focusing on React and modern web development. Here are some insights...", new DateTime(2025, 10, 10, 12, 0, 0, 0, DateTimeKind.Local), 3, "My Frontend Development Journey", null },
                    { 2, 1, "Working with SQL Server and Entity Framework taught me valuable lessons about database design...", new DateTime(2025, 10, 9, 16, 30, 0, 0, DateTimeKind.Local), 4, "Database Design Best Practices", null },
                    { 3, 1, "After trying multiple approaches to business analysis, here are the tools and methodologies that delivered results...", new DateTime(2025, 10, 8, 11, 15, 0, 0, DateTimeKind.Local), 5, "Business Analysis Tools That Actually Work", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
