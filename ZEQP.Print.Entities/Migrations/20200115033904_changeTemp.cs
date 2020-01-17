using Microsoft.EntityFrameworkCore.Migrations;

namespace ZEQP.Print.Entities.Migrations
{
    public partial class changeTemp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Templates",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Templates",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Verstion",
                table: "Templates",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "Verstion",
                table: "Templates");
        }
    }
}
