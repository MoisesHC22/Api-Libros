using AutoMapper;
using gRPC.Libro.Serve;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tienda.Microservicio.Libro.Modelo;
using Tienda.Microservicio.Libro.Persistencia;

namespace Tienda.Microservicio.Libro.Aplicacion
{
    public class Buscar
    {
        public class LibroUnico : IRequest<LibroMaterialDto>
        {
            public string Dato { get; set; }
        }

        public class Manejador : IRequestHandler<LibroUnico, LibroMaterialDto>
        {
            private readonly ContextoLibreria _contexto;
            private readonly IMapper _mapper;
            private readonly LibroImg.LibroImgClient _grpcClient;

            public Manejador(ContextoLibreria contexto, IMapper mapper, LibroImg.LibroImgClient grpcClient)
            {
                _contexto = contexto;
                _mapper = mapper;
                _grpcClient = grpcClient;
            }
            public async Task<LibroMaterialDto> Handle(LibroUnico request, CancellationToken cancellationToken)
            {
                var libros = await _contexto.LibreriaMaterial.
                    Where(x => x.Titulo.Contains(request.Dato)).ToListAsync();

                if (libros == null || libros.Count == 0)
                {
                    throw new Exception("No se encontro el libro");
                }

                var libro = libros.FirstOrDefault();

                var grpcRequest = new IdImg { Id = libro.LibreriaMaterialId.ToString() };
                var grpcResponse = await _grpcClient.ConsultaFiltroAsync(grpcRequest);

                var libroDto = _mapper.Map<LibreriaMaterial, LibroMaterialDto>(libro);

                libroDto.Imagen = grpcResponse.Img;

                return libroDto;
            }
        }
    }
}
