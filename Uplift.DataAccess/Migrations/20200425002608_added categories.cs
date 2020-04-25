using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Uplift.DataAccess.Migrations
{
    public partial class addedcategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "itemCateogry",
                table: "Item",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Email = table.Column<string>(nullable: false),
                    Fname = table.Column<string>(nullable: false),
                    LName = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<long>(nullable: false),
                    Username = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "Offer",
                columns: table => new
                {
                    ItemID = table.Column<Guid>(nullable: false),
                    FName = table.Column<string>(nullable: false),
                    LName = table.Column<string>(nullable: false),
                    BuyerName = table.Column<string>(nullable: false),
                    OfferDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offer", x => x.ItemID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Offer");

            migrationBuilder.DropColumn(
                name: "itemCateogry",
                table: "Item");
        }
    }
}
