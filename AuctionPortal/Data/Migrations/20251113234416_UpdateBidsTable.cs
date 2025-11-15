using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionPortal.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBidsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BidderName",
                table: "Bids",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BidderName",
                table: "Bids");
        }
    }
}
