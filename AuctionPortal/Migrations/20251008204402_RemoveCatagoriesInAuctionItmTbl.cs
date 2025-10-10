using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionPortal.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCatagoriesInAuctionItmTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "AuctionItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "AuctionItems",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
