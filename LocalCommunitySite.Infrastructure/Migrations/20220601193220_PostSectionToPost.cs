using Microsoft.EntityFrameworkCore.Migrations;

namespace LocalCommunitySite.Infrastructure.Migrations
{
    public partial class PostSectionToPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Section",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Section",
                table: "Posts");
        }
    }
}
