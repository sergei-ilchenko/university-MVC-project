using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Soft.Migrations
{
    /// <inheritdoc />
    public partial class player_properties_age_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Players",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Players");
        }
    }
}
