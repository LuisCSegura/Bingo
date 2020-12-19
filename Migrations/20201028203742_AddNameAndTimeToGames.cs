using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bingo.Migrations
{
    public partial class AddNameAndTimeToGames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "games",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "start_time",
                table: "games",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "games");

            migrationBuilder.DropColumn(
                name: "start_time",
                table: "games");
        }
    }
}
