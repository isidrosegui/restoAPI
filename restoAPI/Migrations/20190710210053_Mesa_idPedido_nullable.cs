using Microsoft.EntityFrameworkCore.Migrations;

namespace restoAPI.Migrations
{
    public partial class Mesa_idPedido_nullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IdDetalleAbierto",
                table: "Mesas",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IdDetalleAbierto",
                table: "Mesas",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
