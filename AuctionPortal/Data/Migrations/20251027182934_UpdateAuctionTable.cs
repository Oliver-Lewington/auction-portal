using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionPortal.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAuctionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Auctions",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_CreatorId",
                table: "Auctions",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_AspNetUsers_CreatorId",
                table: "Auctions",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_AspNetUsers_CreatorId",
                table: "Auctions");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_CreatorId",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Auctions");
        }
    }
}
