using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionPortal.Migrations
{
    /// <inheritdoc />
    public partial class AddExpiryDateClmToAuctioItmTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ReservePrice",
                table: "AuctionItems",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryDate",
                table: "AuctionItems",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "AuctionItems");

            migrationBuilder.AlterColumn<decimal>(
                name: "ReservePrice",
                table: "AuctionItems",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);
        }
    }
}
