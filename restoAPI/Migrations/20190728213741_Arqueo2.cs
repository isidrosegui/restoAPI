using Microsoft.EntityFrameworkCore.Migrations;

namespace restoAPI.Migrations
{
    public partial class Arqueo2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalleArqueo_DetallesCaja_DetalleCajaId",
                table: "DetalleArqueo");

            migrationBuilder.DropForeignKey(
                name: "FK_DetalleArqueo_FormasPago_FormaPagoId",
                table: "DetalleArqueo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetalleArqueo",
                table: "DetalleArqueo");

            migrationBuilder.RenameTable(
                name: "DetalleArqueo",
                newName: "DetallesArqueo");

            migrationBuilder.RenameIndex(
                name: "IX_DetalleArqueo_FormaPagoId",
                table: "DetallesArqueo",
                newName: "IX_DetallesArqueo_FormaPagoId");

            migrationBuilder.RenameIndex(
                name: "IX_DetalleArqueo_DetalleCajaId",
                table: "DetallesArqueo",
                newName: "IX_DetallesArqueo_DetalleCajaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetallesArqueo",
                table: "DetallesArqueo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesArqueo_DetallesCaja_DetalleCajaId",
                table: "DetallesArqueo",
                column: "DetalleCajaId",
                principalTable: "DetallesCaja",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesArqueo_FormasPago_FormaPagoId",
                table: "DetallesArqueo",
                column: "FormaPagoId",
                principalTable: "FormasPago",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesArqueo_DetallesCaja_DetalleCajaId",
                table: "DetallesArqueo");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesArqueo_FormasPago_FormaPagoId",
                table: "DetallesArqueo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetallesArqueo",
                table: "DetallesArqueo");

            migrationBuilder.RenameTable(
                name: "DetallesArqueo",
                newName: "DetalleArqueo");

            migrationBuilder.RenameIndex(
                name: "IX_DetallesArqueo_FormaPagoId",
                table: "DetalleArqueo",
                newName: "IX_DetalleArqueo_FormaPagoId");

            migrationBuilder.RenameIndex(
                name: "IX_DetallesArqueo_DetalleCajaId",
                table: "DetalleArqueo",
                newName: "IX_DetalleArqueo_DetalleCajaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetalleArqueo",
                table: "DetalleArqueo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleArqueo_DetallesCaja_DetalleCajaId",
                table: "DetalleArqueo",
                column: "DetalleCajaId",
                principalTable: "DetallesCaja",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleArqueo_FormasPago_FormaPagoId",
                table: "DetalleArqueo",
                column: "FormaPagoId",
                principalTable: "FormasPago",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
