using System.ComponentModel.DataAnnotations;

namespace Tienda.Microservicio.Libro.Modelo
{
    public class LibreriaMaterial
    {
        [Key]
        public Guid? LibreriaMaterialId { get; set; }
        public string Titulo { get; set; }
        public DateTime? FechaPublicacion { get; set;}
        public decimal? Precio { get; set; }
        public decimal? Iva { get; set; }
        public Guid? AutorLibro { get; set; }
        public Guid? Genero { get; set; }
        public int? Cupon { get; set; }
        public string Descripcion { get; set; }
        public decimal? PrecioConIva { get; set; }
    }
}
