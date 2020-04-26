using Microsoft.EntityFrameworkCore.Migrations;

namespace Uplift.DataAccess.Migrations
{
    public partial class changedoffertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BuyerName",
                table: "Offer",
                newName: "sellerEmail");

            migrationBuilder.AlterColumn<string>(
                name: "LName",
                table: "Offer",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "FName",
                table: "Offer",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "buyerEmail",
                table: "Offer",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "buyerEmail",
                table: "Offer");

            migrationBuilder.RenameColumn(
                name: "sellerEmail",
                table: "Offer",
                newName: "BuyerName");

            migrationBuilder.AlterColumn<string>(
                name: "LName",
                table: "Offer",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FName",
                table: "Offer",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
