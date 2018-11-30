using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Auctionator.Data.Migrations
{
    public partial class auction08 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bet_Auctions_AuctionId",
                table: "Bet");

            migrationBuilder.DropForeignKey(
                name: "FK_Bet_AspNetUsers_UserId",
                table: "Bet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bet",
                table: "Bet");

            migrationBuilder.RenameTable(
                name: "Bet",
                newName: "Bets");

            migrationBuilder.RenameIndex(
                name: "IX_Bet_UserId",
                table: "Bets",
                newName: "IX_Bets_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Bet_AuctionId",
                table: "Bets",
                newName: "IX_Bets_AuctionId");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndPayDateTime",
                table: "Auctions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Bets",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bets",
                table: "Bets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bets_Auctions_AuctionId",
                table: "Bets",
                column: "AuctionId",
                principalTable: "Auctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bets_AspNetUsers_UserId",
                table: "Bets",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bets_Auctions_AuctionId",
                table: "Bets");

            migrationBuilder.DropForeignKey(
                name: "FK_Bets_AspNetUsers_UserId",
                table: "Bets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bets",
                table: "Bets");

            migrationBuilder.DropColumn(
                name: "EndPayDateTime",
                table: "Auctions");

            migrationBuilder.RenameTable(
                name: "Bets",
                newName: "Bet");

            migrationBuilder.RenameIndex(
                name: "IX_Bets_UserId",
                table: "Bet",
                newName: "IX_Bet_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Bets_AuctionId",
                table: "Bet",
                newName: "IX_Bet_AuctionId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Bet",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bet",
                table: "Bet",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bet_Auctions_AuctionId",
                table: "Bet",
                column: "AuctionId",
                principalTable: "Auctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bet_AspNetUsers_UserId",
                table: "Bet",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
