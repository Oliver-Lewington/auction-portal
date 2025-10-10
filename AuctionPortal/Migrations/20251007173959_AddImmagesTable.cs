using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionPortal.Migrations
{
    /// <inheritdoc />
    public partial class AddImmagesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuctionItemImage_AuctionItems_AuctionItemId",
                table: "AuctionItemImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuctionItemImage",
                table: "AuctionItemImage");

            migrationBuilder.RenameTable(
                name: "AuctionItemImage",
                newName: "AuctionItemImages");

            migrationBuilder.RenameIndex(
                name: "IX_AuctionItemImage_AuctionItemId",
                table: "AuctionItemImages",
                newName: "IX_AuctionItemImages_AuctionItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuctionItemImages",
                table: "AuctionItemImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionItemImages_AuctionItems_AuctionItemId",
                table: "AuctionItemImages",
                column: "AuctionItemId",
                principalTable: "AuctionItems",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuctionItemImages_AuctionItems_AuctionItemId",
                table: "AuctionItemImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuctionItemImages",
                table: "AuctionItemImages");

            migrationBuilder.RenameTable(
                name: "AuctionItemImages",
                newName: "AuctionItemImage");

            migrationBuilder.RenameIndex(
                name: "IX_AuctionItemImages_AuctionItemId",
                table: "AuctionItemImage",
                newName: "IX_AuctionItemImage_AuctionItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuctionItemImage",
                table: "AuctionItemImage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionItemImage_AuctionItems_AuctionItemId",
                table: "AuctionItemImage",
                column: "AuctionItemId",
                principalTable: "AuctionItems",
                principalColumn: "Id");
        }
    }
}
