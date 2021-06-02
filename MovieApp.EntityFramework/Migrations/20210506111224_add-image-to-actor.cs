using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieApp.EntityFramework.Migrations
{
    public partial class addimagetoactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Actors",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Actors");
        }
    }
}