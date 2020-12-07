using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetManagement.DataAccessLibrary.Migrations
{
    public partial class ComputerRecords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetRecords");

            migrationBuilder.CreateTable(
                name: "ComputerRecords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComputerId = table.Column<string>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    HolderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComputerRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComputerRecords_Computers_ComputerId",
                        column: x => x.ComputerId,
                        principalTable: "Computers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComputerRecords_AssetHolders_HolderId",
                        column: x => x.HolderId,
                        principalTable: "AssetHolders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComputerRecords_ComputerId",
                table: "ComputerRecords",
                column: "ComputerId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputerRecords_HolderId",
                table: "ComputerRecords",
                column: "HolderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComputerRecords");

            migrationBuilder.CreateTable(
                name: "AssetRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComputerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HolderId = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetRecords_Computers_ComputerId",
                        column: x => x.ComputerId,
                        principalTable: "Computers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetRecords_AssetHolders_HolderId",
                        column: x => x.HolderId,
                        principalTable: "AssetHolders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssetRecords_ComputerId",
                table: "AssetRecords",
                column: "ComputerId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetRecords_HolderId",
                table: "AssetRecords",
                column: "HolderId");
        }
    }
}
