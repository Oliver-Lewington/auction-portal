using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionPortal.Migrations
{
    /// <inheritdoc />
    public partial class AlterImagesTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Alt",
                table: "AuctionItemImages",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Caption",
                table: "AuctionItemImages",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alt",
                table: "AuctionItemImages");

            migrationBuilder.DropColumn(
                name: "Caption",
                table: "AuctionItemImages");
        }
    }
}
