using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventManager.Data.Migrations
{
    public partial class moreUserIDAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ArtistID",
                table: "FollowedArtists",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "FollowedArtists",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArtistID",
                table: "FollowedArtists");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "FollowedArtists");
        }
    }
}
