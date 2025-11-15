using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionPortal.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAuctionTable_SaleInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SaleEnd",
                table: "Products",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<int>(
                name: "SaleSequence",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductSaleTimeInterval",
                table: "Auctions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaleSequence",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductSaleTimeInterval",
                table: "Auctions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SaleEnd",
                table: "Products",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);
        }
    }
}
