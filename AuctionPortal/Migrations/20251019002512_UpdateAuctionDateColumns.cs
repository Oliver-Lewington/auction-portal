using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionPortal.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAuctionDateColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Auctions");

            migrationBuilder.AddColumn<DateTime>(
                name: "BeginsAt",
                table: "Auctions",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndsAt",
                table: "Auctions",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BeginsAt",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "EndsAt",
                table: "Auctions");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Auctions",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Auctions",
                type: "timestamp without time zone",
                nullable: true);
        }
    }
}
