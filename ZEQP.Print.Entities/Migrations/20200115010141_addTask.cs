using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZEQP.Print.Entities.Migrations
{
    public partial class addTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "History");

            migrationBuilder.AddColumn<bool>(
                name: "SaveToFile",
                table: "Templates",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "PrintTasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PrintName = table.Column<string>(nullable: true),
                    Copies = table.Column<int>(nullable: false),
                    TemplateId = table.Column<int>(nullable: false),
                    Action = table.Column<int>(nullable: false),
                    IsWait = table.Column<bool>(nullable: false),
                    Query = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    PrintCount = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    ModifyTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrintTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrintTasks_Templates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "Templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrintTasks_TemplateId",
                table: "PrintTasks",
                column: "TemplateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrintTasks");

            migrationBuilder.DropColumn(
                name: "SaveToFile",
                table: "Templates");

            migrationBuilder.CreateTable(
                name: "History",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Action = table.Column<int>(type: "INTEGER", nullable: false),
                    Body = table.Column<string>(type: "TEXT", nullable: true),
                    Copies = table.Column<int>(type: "INTEGER", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsWait = table.Column<bool>(type: "INTEGER", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PrintName = table.Column<string>(type: "TEXT", nullable: true),
                    Query = table.Column<string>(type: "TEXT", nullable: true),
                    TemplateId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_History", x => x.Id);
                    table.ForeignKey(
                        name: "FK_History_Templates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "Templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_History_TemplateId",
                table: "History",
                column: "TemplateId");
        }
    }
}
