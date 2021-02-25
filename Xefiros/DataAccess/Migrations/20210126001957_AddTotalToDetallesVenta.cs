using Microsoft.EntityFrameworkCore.Migrations;

namespace Xefiros.DataAccess.Migrations
{
    public partial class AddTotalToDetallesVenta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "DetallesVenta",
                type: "decimal(11,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "DetallesVenta");
        }
    }
}
