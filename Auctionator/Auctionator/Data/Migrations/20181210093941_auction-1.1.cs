using Microsoft.EntityFrameworkCore.Migrations;

namespace Auctionator.Data.Migrations
{
    public partial class auction11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Auctions_AuctionId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_AuctionId",
                table: "Products");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Products",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<int>(
                name: "AuctionId",
                table: "Products",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Products_AuctionId",
                table: "Products",
                column: "AuctionId",
                unique: true,
                filter: "[AuctionId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Auctions_AuctionId",
                table: "Products",
                column: "AuctionId",
                principalTable: "Auctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Auctions_AuctionId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_AuctionId",
                table: "Products");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Products",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AuctionId",
                table: "Products",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_AuctionId",
                table: "Products",
                column: "AuctionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Auctions_AuctionId",
                table: "Products",
                column: "AuctionId",
                principalTable: "Auctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
