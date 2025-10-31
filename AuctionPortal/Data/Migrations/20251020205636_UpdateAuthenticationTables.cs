using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionPortal.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAuthenticationTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApprovedByAdmin",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApprovedByAdmin",
                table: "AspNetUsers");
        }
    }
}
