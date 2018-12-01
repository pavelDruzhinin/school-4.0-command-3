using Microsoft.EntityFrameworkCore.Migrations;

namespace Auctionator.Data.Migrations
{
    public partial class auction09 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Products",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "ProductPhotos",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "LastBet",
                table: "Auctions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaidStatus",
                table: "Auctions",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastBet",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "PaidStatus",
                table: "Auctions");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Products",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "ProductPhotos",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
