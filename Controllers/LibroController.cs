using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tienda.Microservicio.Libro.Aplicacion;

namespace Tienda.Microservicio.Libro.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LibroController : Controller
    {

        private readonly IMediator _mediator;

        public LibroController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost, Route("Crear")]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            return await _mediator.Send(data);
        }


        [HttpGet, Route("Getlibros")]
        public async Task<ActionResult<List<LibroMaterialDto>>> GetLibros()
        {
            return await _mediator.Send(new Consulta.Ejecuta());
        }


        [HttpGet, Route("GetLibro")]
        public async Task<ActionResult<LibroMaterialDto>> GetLibro(Guid id)
        {
            return await _mediator.Send(new ConsultaFiltro.LibroUnico { LibroId = id });
        }


        [HttpGet, Route("Buscar")]
        public async Task<ActionResult<LibroMaterialDto>> Buscar(string dato)
        {
            return await _mediator.Send(new Buscar.LibroUnico { Dato = dato });
        }
    }
}
