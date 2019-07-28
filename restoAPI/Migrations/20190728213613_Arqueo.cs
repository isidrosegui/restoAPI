using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace restoAPI.Migrations
{
    public partial class Arqueo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DetalleArqueo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FormaPagoId = table.Column<int>(nullable: true),
                    Monto = table.Column<decimal>(nullable: false),
                    DetalleCajaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleArqueo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetalleArqueo_DetallesCaja_DetalleCajaId",
                        column: x => x.DetalleCajaId,
                        principalTable: "DetallesCaja",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleArqueo_FormasPago_FormaPagoId",
                        column: x => x.FormaPagoId,
                        principalTable: "FormasPago",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetalleArqueo_DetalleCajaId",
                table: "DetalleArqueo",
                column: "DetalleCajaId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleArqueo_FormaPagoId",
                table: "DetalleArqueo",
                column: "FormaPagoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalleArqueo");
        }
    }
}
