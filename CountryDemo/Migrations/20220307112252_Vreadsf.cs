using Microsoft.EntityFrameworkCore.Migrations;

namespace CountryDemo.Migrations
{
    public partial class Vreadsf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryName",
                table: "StateData");

            migrationBuilder.AddColumn<int>(
                name: "CoutryId",
                table: "StateData",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoutryId",
                table: "StateData");

            migrationBuilder.AddColumn<string>(
                name: "CountryName",
                table: "StateData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
