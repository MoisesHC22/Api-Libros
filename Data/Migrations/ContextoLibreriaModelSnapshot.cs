﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Tienda.Microservicio.Libro.Persistencia;

#nullable disable

namespace Tienda.Microservicio.Libro.Data.Migrations
{
    [DbContext(typeof(ContextoLibreria))]
    partial class ContextoLibreriaModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.19")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Tienda.Microservicio.Libro.Modelo.LibreriaMaterial", b =>
                {
                    b.Property<Guid?>("LibreriaMaterialId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AutorLibro")
                        .HasColumnType("uuid");

                    b.Property<int?>("Cupon")
                        .HasColumnType("integer");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("FechaPublicacion")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("Genero")
                        .HasColumnType("uuid");

                    b.Property<decimal?>("Iva")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("Precio")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("PrecioConIva")
                        .HasColumnType("numeric");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LibreriaMaterialId");

                    b.ToTable("LibreriaMaterial");
                });
#pragma warning restore 612, 618
        }
    }
}
