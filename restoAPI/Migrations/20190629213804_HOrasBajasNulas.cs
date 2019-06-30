using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace restoAPI.Migrations
{
    public partial class HOrasBajasNulas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "HoraBaja",
                table: "Pedidos",
                nullable: true,
                oldClrType: typeof(TimeSpan));

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "HoraBaja",
                table: "Pagos",
                nullable: true,
                oldClrType: typeof(TimeSpan));

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "HoraBaja",
                table: "DetallesPedido",
                nullable: true,
                oldClrType: typeof(TimeSpan));

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "HoraBaja",
                table: "DetallesMesa",
                nullable: true,
                oldClrType: typeof(TimeSpan));

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "HoraBaja",
                table: "DetallesCaja",
                nullable: true,
                oldClrType: typeof(TimeSpan));

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "HoraBaja",
                table: "Deliveries",
                nullable: true,
                oldClrType: typeof(TimeSpan));

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "HoraBaja",
                table: "Comandas",
                nullable: true,
                oldClrType: typeof(TimeSpan));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "HoraBaja",
                table: "Pedidos",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "HoraBaja",
                table: "Pagos",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "HoraBaja",
                table: "DetallesPedido",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "HoraBaja",
                table: "DetallesMesa",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "HoraBaja",
                table: "DetallesCaja",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "HoraBaja",
                table: "Deliveries",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "HoraBaja",
                table: "Comandas",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldNullable: true);
        }
    }
}
