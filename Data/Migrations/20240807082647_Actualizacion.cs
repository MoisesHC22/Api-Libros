using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tienda.Microservicio.Libro.Data.Migrations
{
    /// <inheritdoc />
    public partial class Actualizacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cupon",
                table: "LibreriaMaterial",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "LibreriaMaterial",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "Genero",
                table: "LibreriaMaterial",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cupon",
                table: "LibreriaMaterial");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "LibreriaMaterial");

            migrationBuilder.DropColumn(
                name: "Genero",
                table: "LibreriaMaterial");
        }
    }
}
