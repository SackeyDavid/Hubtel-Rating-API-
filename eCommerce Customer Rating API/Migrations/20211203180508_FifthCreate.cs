using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce_Customer_Rating_API.Migrations
{
    public partial class FifthCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Auth");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Auth",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Auth",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Auth");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Auth");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Auth",
                type: "nvarchar(10)",
                nullable: true);
        }
    }
}
