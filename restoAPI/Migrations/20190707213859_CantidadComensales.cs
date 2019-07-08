using Microsoft.EntityFrameworkCore.Migrations;

namespace restoAPI.Migrations
{
    public partial class CantidadComensales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "CantidadComensales",
                table: "DetallesMesa",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CantidadComensales",
                table: "DetallesMesa",
                nullable: true,
                oldClrType: typeof(short));
        }
    }
}
