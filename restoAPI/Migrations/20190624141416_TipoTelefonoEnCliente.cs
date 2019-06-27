using Microsoft.EntityFrameworkCore.Migrations;

namespace restoAPI.Migrations
{
    public partial class TipoTelefonoEnCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoTelefono",
                table: "Clientes");

            migrationBuilder.AddColumn<int>(
                name: "TipoTelefonoId",
                table: "Clientes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_TipoTelefonoId",
                table: "Clientes",
                column: "TipoTelefonoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_TiposTelefono_TipoTelefonoId",
                table: "Clientes",
                column: "TipoTelefonoId",
                principalTable: "TiposTelefono",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_TiposTelefono_TipoTelefonoId",
                table: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_TipoTelefonoId",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "TipoTelefonoId",
                table: "Clientes");

            migrationBuilder.AddColumn<string>(
                name: "TipoTelefono",
                table: "Clientes",
                nullable: true);
        }
    }
}
