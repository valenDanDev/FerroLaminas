using API_FerroLaminas.DTOs;
using API_FerroLaminas.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_FerroLaminas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoOrdenTrabajoController : ControllerBase
    {
        private readonly EstadoOrdenTrabajoService _service;

        public EstadoOrdenTrabajoController(EstadoOrdenTrabajoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<EstadoOrdenTrabajoDTO>>> GetAllEstados()
        {
            var estados = await _service.GetAllEstadosAsync();
            return Ok(estados);
        }
    }
}
