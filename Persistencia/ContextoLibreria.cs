
using Microsoft.EntityFrameworkCore;
using Tienda.Microservicio.Libro.Modelo;

namespace Tienda.Microservicio.Libro.Persistencia
{
    public class ContextoLibreria : DbContext
    {
        public ContextoLibreria(DbContextOptions<ContextoLibreria> options) :  base(options) 
        {
        }

        public DbSet<LibreriaMaterial> LibreriaMaterial { get; set; }
    }
}
