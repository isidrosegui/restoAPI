using Microsoft.EntityFrameworkCore.Migrations;

namespace restoAPI.Migrations
{
    public partial class SinPrecioActual : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Precios_PrecioActualId",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_PrecioActualId",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "PrecioActualId",
                table: "Productos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrecioActualId",
                table: "Productos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_PrecioActualId",
                table: "Productos",
                column: "PrecioActualId");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Precios_PrecioActualId",
                table: "Productos",
                column: "PrecioActualId",
                principalTable: "Precios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
