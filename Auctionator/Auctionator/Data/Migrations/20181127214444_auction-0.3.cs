using Microsoft.EntityFrameworkCore.Migrations;

namespace Auctionator.Data.Migrations
{
    public partial class auction03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Subscribers_SubscriberUserId",
                table: "Auctions");

            migrationBuilder.DropTable(
                name: "Subscribers");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_SubscriberUserId",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "SubscriberUserId",
                table: "Auctions");

            migrationBuilder.CreateTable(
                name: "SubscribedAuctions",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    AuctionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscribedAuctions", x => new { x.UserId, x.AuctionId });
                    table.ForeignKey(
                        name: "FK_SubscribedAuctions_Auctions_AuctionId",
                        column: x => x.AuctionId,
                        principalTable: "Auctions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubscribedAuctions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubscribedAuctions_AuctionId",
                table: "SubscribedAuctions",
                column: "AuctionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscribedAuctions");

            migrationBuilder.AddColumn<string>(
                name: "SubscriberUserId",
                table: "Auctions",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Subscribers",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    AuctionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribers", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Subscribers_Auctions_AuctionId",
                        column: x => x.AuctionId,
                        principalTable: "Auctions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Subscribers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_SubscriberUserId",
                table: "Auctions",
                column: "SubscriberUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscribers_AuctionId",
                table: "Subscribers",
                column: "AuctionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_Subscribers_SubscriberUserId",
                table: "Auctions",
                column: "SubscriberUserId",
                principalTable: "Subscribers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
