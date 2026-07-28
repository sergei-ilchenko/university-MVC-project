using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Soft.Migrations
{
    /// <inheritdoc />
    public partial class team_changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Player1",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Player2",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Player3",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Player4",
                table: "Teams");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Player1",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Player2",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Player3",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Player4",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
