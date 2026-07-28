using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Soft.Migrations
{
    /// <inheritdoc />
    public partial class player_properties_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Players",
                newName: "Nick");

            migrationBuilder.AddColumn<DateTime>(
                name: "Born",
                table: "Players",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CurrentTeam",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Born",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "CurrentTeam",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "Nick",
                table: "Players",
                newName: "Country");
        }
    }
}
