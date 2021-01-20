using Microsoft.EntityFrameworkCore.Migrations;

namespace Xefiros.DataAccess.Migrations
{
    public partial class FixCedulaClienteTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Clientes_Cedula",
                table: "Clientes",
                column: "Cedula",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Clientes_Cedula",
                table: "Clientes");
        }
    }
}
