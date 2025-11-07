using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionPortal.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAuctionsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "Auctions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_ImageId",
                table: "Auctions",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_Images_ImageId",
                table: "Auctions",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Images_ImageId",
                table: "Auctions");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_ImageId",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Auctions");
        }
    }
}
