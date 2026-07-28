using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Soft.Migrations
{
    /// <inheritdoc />
    public partial class player_team_connect_test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentTeam",
                table: "Players");

            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Teams");

            migrationBuilder.AddColumn<string>(
                name: "CurrentTeam",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
