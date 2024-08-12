using FluentValidation;
using gRPC.Libro.Serve;
using MediatR;
using System.Data;
using Tienda.Microservicio.Libro.Modelo;
using Tienda.Microservicio.Libro.Persistencia;

namespace Tienda.Microservicio.Libro.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string Titulo { get; set; }
            public DateTime? FechaPublicacion { get; set; }
            public decimal? Precio { get; set; }
            public Guid? AutorLibro { get; set; }
            public string Imagen { get; set; }
            public Guid? Genero { get; set; }
            public int? Cupon { get; set; }
            public string Descripcion { get; set; }
        }

        public class EjecutaValidation : AbstractValidator<Ejecuta>
        {
            public EjecutaValidation()
            {
                RuleFor(x => x.Titulo).NotEmpty();
                RuleFor(x => x.FechaPublicacion).NotEmpty();
                RuleFor(x => x.Precio).NotEmpty();
                RuleFor(x => x.AutorLibro).NotEmpty();
                RuleFor(x => x.Imagen).NotEmpty();
                RuleFor(x => x.Genero).NotEmpty();
                RuleFor(x => x.Cupon).NotEmpty();
                RuleFor(x => x.Descripcion).NotEmpty();
            }

            public class Manejador : IRequestHandler<Ejecuta>
            {
                private readonly ContextoLibreria _contexto;
                private readonly ILogger<Manejador> _logger;
                private readonly LibroImg.LibroImgClient _grpcClient;

                public Manejador(ContextoLibreria contexto, ILogger<Manejador> logger,LibroImg.LibroImgClient grpcClient)
                {
                    _contexto = contexto;
                    _grpcClient = grpcClient;
                    _logger = logger;
                }

                public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
                {
                    var ValorIva = 0.16;
                    var PrecioFinal = (request.Precio * (decimal)ValorIva) + request.Precio;

                    var libro = new LibreriaMaterial
                    {
                        Titulo = request.Titulo,
                        FechaPublicacion = request.FechaPublicacion,
                        Precio = request.Precio,
                        AutorLibro = request.AutorLibro,
                        Iva = request.Precio * (decimal)ValorIva,
                        PrecioConIva = PrecioFinal,
                        Genero = request.Genero,
                        Cupon = request.Cupon,
                        Descripcion = request.Descripcion
                    };

                    _contexto.LibreriaMaterial.Add(libro);
                    var valor = await _contexto.SaveChangesAsync();

                    if (valor > 0)
                    {
                        var grpcRequest = new ImgRequest
                        {
                            Id = libro.LibreriaMaterialId.ToString(),
                            Img = request.Imagen
                        };

                        var grpcResponse = await _grpcClient.GuardarImgAsync(grpcRequest);

                        if (grpcResponse.Mensaje != "La imagen se guardo exitosamente")
                        {
                            throw new Exception("No se pudo guardar la imagen");
                        }
                    }
                    return Unit.Value;
                }
            }


        }

    }
}
