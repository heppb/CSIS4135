using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EventManager.Data.Migrations
{
    public partial class followedArtists : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FollowedArtists",
                columns: table => new
                {
                    FollowedArtistsID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowedArtists", x => x.FollowedArtistsID);
                });

            migrationBuilder.AddColumn<int>(
                name: "FollowedArtistsID",
                table: "AspNetUsers",
                nullable: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_FollowedArtists_FollowedArtistsID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FollowedArtistsID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FollowedArtistsID",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "FollowedArtists");
        }
    }
}
