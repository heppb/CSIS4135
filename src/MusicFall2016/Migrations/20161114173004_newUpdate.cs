using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicFall2016.Migrations
{
    public partial class newUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlbumID",
                table: "Playlists");

            migrationBuilder.CreateTable(
                name: "PlaylistConnect",
                columns: table => new
                {
                    PlaylistID = table.Column<int>(nullable: false),
                    AlbumID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaylistConnect", x => new { x.PlaylistID, x.AlbumID });
                    table.ForeignKey(
                        name: "FK_PlaylistConnect_Albums_AlbumID",
                        column: x => x.AlbumID,
                        principalTable: "Albums",
                        principalColumn: "AlbumID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaylistConnect_Playlists_PlaylistID",
                        column: x => x.PlaylistID,
                        principalTable: "Playlists",
                        principalColumn: "PlaylistID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddColumn<string>(
                name: "PlaylistName",
                table: "Playlists",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Playlists",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Playlists",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_UserId",
                table: "Playlists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistConnect_AlbumID",
                table: "PlaylistConnect",
                column: "AlbumID");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistConnect_PlaylistID",
                table: "PlaylistConnect",
                column: "PlaylistID");

            migrationBuilder.AddForeignKey(
                name: "FK_Playlists_AspNetUsers_UserId",
                table: "Playlists",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Playlists_AspNetUsers_UserId",
                table: "Playlists");

            migrationBuilder.DropIndex(
                name: "IX_Playlists_UserId",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "PlaylistName",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Playlists");

            migrationBuilder.DropTable(
                name: "PlaylistConnect");

            migrationBuilder.AddColumn<int>(
                name: "AlbumID",
                table: "Playlists",
                nullable: false,
                defaultValue: 0);
        }
    }
}
