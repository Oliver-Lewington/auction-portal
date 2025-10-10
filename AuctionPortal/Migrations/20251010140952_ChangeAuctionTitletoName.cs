using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionPortal.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAuctionTitletoName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Auctions",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Auctions",
                newName: "Title");
        }
    }
}
