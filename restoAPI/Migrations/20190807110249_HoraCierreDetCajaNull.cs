using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace restoAPI.Migrations
{
    public partial class HoraCierreDetCajaNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "HoraCierre",
                table: "DetallesCaja",
                nullable: true,
                oldClrType: typeof(TimeSpan));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCierreArqueo",
                table: "ArqueoCajas",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "HoraCierreArqueo",
                table: "ArqueoCajas",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaCierreArqueo",
                table: "ArqueoCajas");

            migrationBuilder.DropColumn(
                name: "HoraCierreArqueo",
                table: "ArqueoCajas");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "HoraCierre",
                table: "DetallesCaja",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldNullable: true);
        }
    }
}
