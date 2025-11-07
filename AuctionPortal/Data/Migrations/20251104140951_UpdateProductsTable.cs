using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionPortal.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProductModelId",
                table: "Images",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_ProductModelId",
                table: "Images",
                column: "ProductModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Products_ProductModelId",
                table: "Images",
                column: "ProductModelId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Products_ProductModelId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_ProductModelId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ProductModelId",
                table: "Images");
        }
    }
}
