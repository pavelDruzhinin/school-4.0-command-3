using Microsoft.EntityFrameworkCore.Migrations;

namespace Auctionator.Data.Migrations
{
    public partial class auction09 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "ProductPhotos",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "ProductPhotos",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
