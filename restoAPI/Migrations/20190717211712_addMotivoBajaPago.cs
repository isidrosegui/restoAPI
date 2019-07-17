using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace restoAPI.Migrations
{
    public partial class addMotivoBajaPago : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Productos",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "MotivoBaja",
                table: "Pagos",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Precios_Id",
                table: "Productos",
                column: "Id",
                principalTable: "Precios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Precios_Id",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "MotivoBaja",
                table: "Pagos");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Productos",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
        }
    }
}
