using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetManagement.DataAccessLibrary.Migrations
{
    public partial class ModelsAsList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssetHolders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Username = table.Column<string>(nullable: false),
                    Department = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetHolders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Computers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    PcName = table.Column<string>(nullable: false),
                    OperatingSystem = table.Column<string>(nullable: true),
                    Manufacturer = table.Column<string>(nullable: true),
                    SerialNumber = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Computers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComputerModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ComputerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComputerModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComputerModel_Computers_ComputerId",
                        column: x => x.ComputerId,
                        principalTable: "Computers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "IX_ComputerModel_ComputerId",
                table: "ComputerModel",
                column: "ComputerId");

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
                name: "ComputerModel");

            migrationBuilder.DropTable(
                name: "ComputerRecords");

            migrationBuilder.DropTable(
                name: "Computers");

            migrationBuilder.DropTable(
                name: "AssetHolders");
        }
    }
}
