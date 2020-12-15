using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetManagement.DataAccessLibrary.Migrations
{
    public partial class AddedPcAdStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PcName",
                table: "Computers",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AddColumn<string>(
                name: "PcAdStatus",
                table: "Computers",
                maxLength: 30,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PcAdStatus",
                table: "Computers");

            migrationBuilder.AlterColumn<string>(
                name: "PcName",
                table: "Computers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);
        }
    }
}
