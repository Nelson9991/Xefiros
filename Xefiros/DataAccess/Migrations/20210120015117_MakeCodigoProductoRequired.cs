using Microsoft.EntityFrameworkCore.Migrations;

namespace Xefiros.DataAccess.Migrations
{
    public partial class MakeCodigoProductoRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Productos_Codigo",
                table: "Productos");

            migrationBuilder.AlterColumn<string>(
                name: "Codigo",
                table: "Productos",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_Codigo",
                table: "Productos",
                column: "Codigo",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Productos_Codigo",
                table: "Productos");

            migrationBuilder.AlterColumn<string>(
                name: "Codigo",
                table: "Productos",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_Codigo",
                table: "Productos",
                column: "Codigo",
                unique: true,
                filter: "[Codigo] IS NOT NULL");
        }
    }
}
