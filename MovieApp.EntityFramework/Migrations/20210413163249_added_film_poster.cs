using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieApp.EntityFramework.Migrations
{
    public partial class added_film_poster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "PosterImageData",
                table: "Films",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PosterImageData",
                table: "Films");
        }
    }
}
