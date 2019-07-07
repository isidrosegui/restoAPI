using Microsoft.EntityFrameworkCore.Migrations;

namespace restoAPI.Migrations
{
    public partial class ArreglosMesayDetallesMesa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mesas_DetallesMesa_DetalleAbiertoMesaId",
                table: "Mesas");

            migrationBuilder.DropForeignKey(
                name: "FK_Mesas_Pedidos_PedidoAbiertoId",
                table: "Mesas");

            migrationBuilder.DropIndex(
                name: "IX_Mesas_DetalleAbiertoMesaId",
                table: "Mesas");

            migrationBuilder.DropIndex(
                name: "IX_Mesas_PedidoAbiertoId",
                table: "Mesas");

            migrationBuilder.DropColumn(
                name: "DetalleAbiertoMesaId",
                table: "Mesas");

            migrationBuilder.DropColumn(
                name: "PedidoAbiertoId",
                table: "Mesas");

            migrationBuilder.DropColumn(
                name: "IdMesa",
                table: "DetallesMesa");

            migrationBuilder.AddColumn<int>(
                name: "IdDetalleAbierto",
                table: "Mesas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PedidoId",
                table: "DetallesMesa",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetallesMesa_PedidoId",
                table: "DetallesMesa",
                column: "PedidoId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesMesa_Pedidos_PedidoId",
                table: "DetallesMesa",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesMesa_Pedidos_PedidoId",
                table: "DetallesMesa");

            migrationBuilder.DropIndex(
                name: "IX_DetallesMesa_PedidoId",
                table: "DetallesMesa");

            migrationBuilder.DropColumn(
                name: "IdDetalleAbierto",
                table: "Mesas");

            migrationBuilder.DropColumn(
                name: "PedidoId",
                table: "DetallesMesa");

            migrationBuilder.AddColumn<int>(
                name: "DetalleAbiertoMesaId",
                table: "Mesas",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PedidoAbiertoId",
                table: "Mesas",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdMesa",
                table: "DetallesMesa",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Mesas_DetalleAbiertoMesaId",
                table: "Mesas",
                column: "DetalleAbiertoMesaId");

            migrationBuilder.CreateIndex(
                name: "IX_Mesas_PedidoAbiertoId",
                table: "Mesas",
                column: "PedidoAbiertoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mesas_DetallesMesa_DetalleAbiertoMesaId",
                table: "Mesas",
                column: "DetalleAbiertoMesaId",
                principalTable: "DetallesMesa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Mesas_Pedidos_PedidoAbiertoId",
                table: "Mesas",
                column: "PedidoAbiertoId",
                principalTable: "Pedidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
