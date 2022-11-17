using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class MerchantAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PictureURL",
                table: "Products",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "MerchantUserId",
                table: "Products",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<long>(
                name: "OrderDate",
                table: "Orders",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "TEXT");

            migrationBuilder.CreateTable(
                name: "MerchantUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Password = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MerchantUsers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_MerchantUserId",
                table: "Products",
                column: "MerchantUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_MerchantUsers_MerchantUserId",
                table: "Products",
                column: "MerchantUserId",
                principalTable: "MerchantUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_MerchantUsers_MerchantUserId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "MerchantUsers");

            migrationBuilder.DropIndex(
                name: "IX_Products_MerchantUserId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MerchantUserId",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "PictureURL",
                table: "Products",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "OrderDate",
                table: "Orders",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "INTEGER");
        }
    }
}
