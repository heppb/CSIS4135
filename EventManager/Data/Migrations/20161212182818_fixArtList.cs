using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventManager.Data.Migrations
{
    public partial class fixArtList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserOfList",
                table: "FollowedArtists");

            migrationBuilder.AddColumn<string>(
                name: "UserOfListId",
                table: "FollowedArtists",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FollowedArtistsID",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FollowedArtists_UserOfListId",
                table: "FollowedArtists",
                column: "UserOfListId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FollowedArtistsID",
                table: "AspNetUsers",
                column: "FollowedArtistsID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_FollowedArtists_FollowedArtistsID",
                table: "AspNetUsers",
                column: "FollowedArtistsID",
                principalTable: "FollowedArtists",
                principalColumn: "FollowedArtistsID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FollowedArtists_AspNetUsers_UserOfListId",
                table: "FollowedArtists",
                column: "UserOfListId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_FollowedArtists_FollowedArtistsID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_FollowedArtists_AspNetUsers_UserOfListId",
                table: "FollowedArtists");

            migrationBuilder.DropIndex(
                name: "IX_FollowedArtists_UserOfListId",
                table: "FollowedArtists");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FollowedArtistsID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserOfListId",
                table: "FollowedArtists");

            migrationBuilder.DropColumn(
                name: "FollowedArtistsID",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "UserOfList",
                table: "FollowedArtists",
                nullable: true);
        }
    }
}
