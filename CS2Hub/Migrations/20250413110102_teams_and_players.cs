using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Soft.Migrations
{
    /// <inheritdoc />
    public partial class teams_and_players : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tournaments",
                table: "Tournaments");

            migrationBuilder.RenameTable(
                name: "Tournaments",
                newName: "TourNData");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TourNData",
                table: "TourNData",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TourNData",
                table: "TourNData");

            migrationBuilder.RenameTable(
                name: "TourNData",
                newName: "Tournaments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tournaments",
                table: "Tournaments",
                column: "Id");
        }
    }
}
