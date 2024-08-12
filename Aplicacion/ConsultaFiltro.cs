using AutoMapper;
using gRPC.Libro.Serve;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Tienda.Microservicio.Libro.Modelo;
using Tienda.Microservicio.Libro.Persistencia;

namespace Tienda.Microservicio.Libro.Aplicacion
{
    public class ConsultaFiltro
    {
        public class LibroUnico : IRequest<LibroMaterialDto>
        {
            public Guid? LibroId { get; set; }
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
                var libro = await _contexto.LibreriaMaterial.
                    Where(x => x.LibreriaMaterialId == request.LibroId).FirstOrDefaultAsync();
                if(libro == null) 
                {
                    throw new Exception("No se encontro el libro");
                }

                var grpcRequest = new IdImg { Id = libro.LibreriaMaterialId.ToString() };
                var grpcResponse = await _grpcClient.ConsultaFiltroAsync(grpcRequest);

                var libroDto = _mapper.Map<LibreriaMaterial, LibroMaterialDto>(libro);

                libroDto.Imagen = grpcResponse.Img;

                return libroDto;
            }

        }
    }
}
