using Microsoft.EntityFrameworkCore.Migrations;

namespace MacroCenter.Migrations
{
    public partial class ImageStrings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageString",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageString",
                table: "Products");
        }
    }
}
