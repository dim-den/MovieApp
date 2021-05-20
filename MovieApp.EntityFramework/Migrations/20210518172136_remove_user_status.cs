using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieApp.EntityFramework.Migrations
{
    public partial class remove_user_status : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
