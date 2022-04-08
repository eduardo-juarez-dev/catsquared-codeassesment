using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestAPI.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Summaries",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CelsiusLow = table.Column<int>(type: "int", nullable: true),
                    CelsiusHigh = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Summaries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Forecasts",
                columns: table => new
                {
                    Id = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Celsius = table.Column<int>(type: "int", nullable: false),
                    SummaryId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forecasts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Forecasts_Summaries_SummaryId",
                        column: x => x.SummaryId,
                        principalTable: "Summaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Summaries",
                columns: new[] { "Id", "CelsiusHigh", "CelsiusLow" },
                values: new object[,]
                {
                    { "Freezing", 5, null },
                    { "Chilly", 10, 5 },
                    { "Cool", 15, 10 },
                    { "Mild", 20, 15 },
                    { "Warm", 25, 20 },
                    { "Balmy", 30, 25 },
                    { "Hot", 35, 30 },
                    { "Sweltering", 40, 35 },
                    { "Scorching", null, 40 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Forecasts_SummaryId",
                table: "Forecasts",
                column: "SummaryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Forecasts");

            migrationBuilder.DropTable(
                name: "Summaries");
        }
    }
}
