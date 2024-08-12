using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tienda.Microservicio.Libro.Data.Migrations
{
    /// <inheritdoc />
    public partial class ActualizarDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Iva",
                table: "LibreriaMaterial",
                type: "numeric",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Iva",
                table: "LibreriaMaterial");
        }
    }
}
