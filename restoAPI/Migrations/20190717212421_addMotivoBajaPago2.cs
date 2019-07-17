﻿using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace restoAPI.Migrations
{
    public partial class addMotivoBajaPago2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Precios_Id",
                table: "Productos");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Productos",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Productos",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Precios_Id",
                table: "Productos",
                column: "Id",
                principalTable: "Precios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}