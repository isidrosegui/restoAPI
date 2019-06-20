﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace restoAPI.Migrations
{
    public partial class PrecioProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrecioActual",
                table: "Productos");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioActual",
                table: "Productos",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
