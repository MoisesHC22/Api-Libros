using AutoMapper;
using gRPC.Libro.Serve;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Tienda.Microservicio.Libro.Persistencia;

namespace Tienda.Microservicio.Libro.Aplicacion
{
    public class Consulta
    {
        public class Ejecuta : IRequest<List<LibroMaterialDto>>
        {
            public Ejecuta()
            {
            }

        }

        public class Manejador : IRequestHandler<Ejecuta, List<LibroMaterialDto>>
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


            public async Task<List<LibroMaterialDto>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var libros = await _contexto.LibreriaMaterial.ToListAsync();
                var librosDto = _mapper.Map<List<LibroMaterialDto>>(libros);

                foreach (var libro in librosDto)
                {
                    var grpcRequest = new IdImg { Id = libro.LibreriaMaterialId.ToString() };

                    try {
                        var grpcResponse = await _grpcClient.ConsultaFiltroAsync(grpcRequest);
                        libro.Imagen = grpcResponse.Img;
                    }
                    catch (Exception ex) 
                    {
                        libro.Imagen = "Imagen no disponible";
                    }
                }

                return librosDto;

            }
        }

    }

}