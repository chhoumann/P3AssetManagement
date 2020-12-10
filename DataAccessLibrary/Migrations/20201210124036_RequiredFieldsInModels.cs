using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetManagement.DataAccessLibrary.Migrations
{
    public partial class RequiredFieldsInModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComputerModel_Computers_ComputerId",
                table: "ComputerModel");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ComputerModel",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ComputerId",
                table: "ComputerModel",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ComputerModel_Computers_ComputerId",
                table: "ComputerModel",
                column: "ComputerId",
                principalTable: "Computers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComputerModel_Computers_ComputerId",
                table: "ComputerModel");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ComputerModel",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ComputerId",
                table: "ComputerModel",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_ComputerModel_Computers_ComputerId",
                table: "ComputerModel",
                column: "ComputerId",
                principalTable: "Computers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
