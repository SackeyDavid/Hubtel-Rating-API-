using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce_Customer_Rating_API.Migrations
{
    public partial class ThirdCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.RenameColumn(
                name: "stars",
                table: "Ratings",
                newName: "Stars");

            migrationBuilder.RenameColumn(
                name: "itemId",
                table: "Ratings",
                newName: "ItemId");

            migrationBuilder.RenameColumn(
                name: "comment",
                table: "Ratings",
                newName: "Comment");

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.UserId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.RenameColumn(
                name: "Stars",
                table: "Ratings",
                newName: "stars");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "Ratings",
                newName: "itemId");

            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "Ratings",
                newName: "comment");

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    phoneNumber = table.Column<string>(type: "nvarchar(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.UserId);
                });
        }
    }
}
