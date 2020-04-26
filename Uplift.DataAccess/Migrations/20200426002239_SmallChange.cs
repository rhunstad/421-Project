using Microsoft.EntityFrameworkCore.Migrations;

namespace Uplift.DataAccess.Migrations
{
    public partial class SmallChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "itemCateogry",
                table: "Item",
                newName: "ItemCategory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ItemCategory",
                table: "Item",
                newName: "itemCateogry");
        }
    }
}
