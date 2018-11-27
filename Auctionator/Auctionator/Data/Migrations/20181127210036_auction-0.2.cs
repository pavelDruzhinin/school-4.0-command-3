using Microsoft.EntityFrameworkCore.Migrations;

namespace Auctionator.Data.Migrations
{
    public partial class auction02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Winners_WinnerId",
                table: "Auctions");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Buyers_BuyerId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Owners_OwnerId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Buyers");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.DropTable(
                name: "Winners");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_AspNetUsers_WinnerId",
                table: "Auctions",
                column: "WinnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_BuyerId",
                table: "Products",
                column: "BuyerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_OwnerId",
                table: "Products",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_AspNetUsers_WinnerId",
                table: "Auctions");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_BuyerId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_OwnerId",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "Buyers",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buyers", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Buyers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Owners_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Winners",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Winners", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Winners_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_Winners_WinnerId",
                table: "Auctions",
                column: "WinnerId",
                principalTable: "Winners",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Buyers_BuyerId",
                table: "Products",
                column: "BuyerId",
                principalTable: "Buyers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Owners_OwnerId",
                table: "Products",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
