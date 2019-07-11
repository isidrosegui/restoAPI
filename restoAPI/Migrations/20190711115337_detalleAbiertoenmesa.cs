using Microsoft.EntityFrameworkCore.Migrations;

namespace restoAPI.Migrations
{
    public partial class detalleAbiertoenmesa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comandas_Pedidos_PedidoId",
                table: "Comandas");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesMesa_Mesas_MesaId",
                table: "DetallesMesa");

            migrationBuilder.DropIndex(
                name: "IX_DetallesMesa_MesaId",
                table: "DetallesMesa");

            migrationBuilder.DropColumn(
                name: "IdDetalleAbierto",
                table: "Mesas");

            migrationBuilder.DropColumn(
                name: "MesaId",
                table: "DetallesMesa");

            migrationBuilder.AddColumn<int>(
                name: "DetalleAbiertoId",
                table: "Mesas",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdMesa",
                table: "DetallesMesa",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "PedidoId",
                table: "Comandas",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mesas_DetalleAbiertoId",
                table: "Mesas",
                column: "DetalleAbiertoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comandas_Pedidos_PedidoId",
                table: "Comandas",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Mesas_DetallesMesa_DetalleAbiertoId",
                table: "Mesas",
                column: "DetalleAbiertoId",
                principalTable: "DetallesMesa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comandas_Pedidos_PedidoId",
                table: "Comandas");

            migrationBuilder.DropForeignKey(
                name: "FK_Mesas_DetallesMesa_DetalleAbiertoId",
                table: "Mesas");

            migrationBuilder.DropIndex(
                name: "IX_Mesas_DetalleAbiertoId",
                table: "Mesas");

            migrationBuilder.DropColumn(
                name: "DetalleAbiertoId",
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
                name: "MesaId",
                table: "DetallesMesa",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PedidoId",
                table: "Comandas",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_DetallesMesa_MesaId",
                table: "DetallesMesa",
                column: "MesaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comandas_Pedidos_PedidoId",
                table: "Comandas",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesMesa_Mesas_MesaId",
                table: "DetallesMesa",
                column: "MesaId",
                principalTable: "Mesas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
