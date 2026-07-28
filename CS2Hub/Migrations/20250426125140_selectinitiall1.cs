using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Soft.Migrations
{
    /// <inheritdoc />
    public partial class selectinitiall1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentEntries_Teams_TeamId",
                table: "TournamentEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentEntries_Tournaments_TourNId",
                table: "TournamentEntries");

            migrationBuilder.DropIndex(
                name: "IX_TournamentEntries_TeamId",
                table: "TournamentEntries");

            migrationBuilder.DropIndex(
                name: "IX_TournamentEntries_TourNId_TeamId",
                table: "TournamentEntries");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TournamentEntries_TeamId",
                table: "TournamentEntries",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentEntries_TourNId_TeamId",
                table: "TournamentEntries",
                columns: new[] { "TourNId", "TeamId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentEntries_Teams_TeamId",
                table: "TournamentEntries",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentEntries_Tournaments_TourNId",
                table: "TournamentEntries",
                column: "TourNId",
                principalTable: "Tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
