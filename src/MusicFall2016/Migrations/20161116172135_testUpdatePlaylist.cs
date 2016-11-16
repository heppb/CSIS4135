using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicFall2016.Migrations
{
    public partial class testUpdatePlaylist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlaylistName",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Playlists");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlaylistName",
                table: "Playlists",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Playlists",
                nullable: false,
                defaultValue: 0);
        }
    }
}
