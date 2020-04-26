using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Uplift.DataAccess.Migrations
{
    public partial class addedguidkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");

            migrationBuilder.RenameColumn(
                name: "Seller",
                table: "Offer",
                newName: "SellerID");

            migrationBuilder.RenameColumn(
                name: "Buyer",
                table: "Offer",
                newName: "BuyerID");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Item",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<Guid>(
                name: "SellerID",
                table: "Item",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Customer",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<Guid>(
                name: "SellerID",
                table: "Customer",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "SellerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "SellerID",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "SellerID",
                table: "Customer");

            migrationBuilder.RenameColumn(
                name: "SellerID",
                table: "Offer",
                newName: "Seller");

            migrationBuilder.RenameColumn(
                name: "BuyerID",
                table: "Offer",
                newName: "Buyer");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Item",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Customer",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "Email");
        }
    }
}
