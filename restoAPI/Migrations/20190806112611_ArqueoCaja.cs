using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace restoAPI.Migrations
{
    public partial class ArqueoCaja : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaAlta",
                table: "DetallesArqueo",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaBaja",
                table: "DetallesArqueo",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "HoraAlta",
                table: "DetallesArqueo",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "HoraBaja",
                table: "DetallesArqueo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Observaciones",
                table: "DetallesArqueo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaAlta",
                table: "DetallesArqueo");

            migrationBuilder.DropColumn(
                name: "FechaBaja",
                table: "DetallesArqueo");

            migrationBuilder.DropColumn(
                name: "HoraAlta",
                table: "DetallesArqueo");

            migrationBuilder.DropColumn(
                name: "HoraBaja",
                table: "DetallesArqueo");

            migrationBuilder.DropColumn(
                name: "Observaciones",
                table: "DetallesArqueo");
        }
    }
}
