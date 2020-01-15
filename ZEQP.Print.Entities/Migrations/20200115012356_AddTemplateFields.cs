using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZEQP.Print.Entities.Migrations
{
    public partial class AddTemplateFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TemplateFields",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TemplateId = table.Column<int>(nullable: false),
                    TableName = table.Column<string>(maxLength: 50, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    FieldType = table.Column<int>(nullable: false),
                    DefaultValue = table.Column<string>(maxLength: 500, nullable: true),
                    imgType = table.Column<int>(nullable: true),
                    ImgWidth = table.Column<int>(nullable: false),
                    ImgHeight = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    ModifyTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemplateFields_Templates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "Templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TemplateFields_TemplateId",
                table: "TemplateFields",
                column: "TemplateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TemplateFields");
        }
    }
}
