using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tienda.Microservicio.Libro.Data.Migrations
{
    /// <inheritdoc />
    public partial class SegundaActualizacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PrecioConIva",
                table: "LibreriaMaterial",
                type: "numeric",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrecioConIva",
                table: "LibreriaMaterial");
        }
    }
}
