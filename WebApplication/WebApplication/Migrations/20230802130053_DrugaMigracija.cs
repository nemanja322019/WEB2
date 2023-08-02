using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication.Migrations
{
    public partial class DrugaMigracija : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Seller_SellerId",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Seller",
                table: "Seller");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Seller");

            migrationBuilder.RenameTable(
                name: "Seller",
                newName: "Sellers");

            migrationBuilder.RenameIndex(
                name: "IX_Seller_Username",
                table: "Sellers",
                newName: "IX_Sellers_Username");

            migrationBuilder.RenameIndex(
                name: "IX_Seller_Email",
                table: "Sellers",
                newName: "IX_Sellers_Email");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Items",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sellers",
                table: "Sellers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Sellers_SellerId",
                table: "Items",
                column: "SellerId",
                principalTable: "Sellers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Sellers_SellerId",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sellers",
                table: "Sellers");

            migrationBuilder.RenameTable(
                name: "Sellers",
                newName: "Seller");

            migrationBuilder.RenameIndex(
                name: "IX_Sellers_Username",
                table: "Seller",
                newName: "IX_Seller_Username");

            migrationBuilder.RenameIndex(
                name: "IX_Sellers_Email",
                table: "Seller",
                newName: "IX_Seller_Email");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Items",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Customers",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Admins",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Seller",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seller",
                table: "Seller",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Seller_SellerId",
                table: "Items",
                column: "SellerId",
                principalTable: "Seller",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
