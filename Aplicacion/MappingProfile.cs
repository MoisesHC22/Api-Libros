using AutoMapper;
using Tienda.Microservicio.Libro.Modelo;

namespace Tienda.Microservicio.Libro.Aplicacion
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<LibreriaMaterial, LibroMaterialDto>();
        }
    }
}
