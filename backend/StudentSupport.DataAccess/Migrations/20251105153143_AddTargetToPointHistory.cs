using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentSupport.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddTargetToPointHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TargetId",
                table: "PointHistories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TargetType",
                table: "PointHistories",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TargetId",
                table: "PointHistories");

            migrationBuilder.DropColumn(
                name: "TargetType",
                table: "PointHistories");
        }
    }
}
