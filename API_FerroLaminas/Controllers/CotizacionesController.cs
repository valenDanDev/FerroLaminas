using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using API_FerroLaminas.Models;
using API_FerroLaminas.Services;

namespace API_FerroLaminas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CotizacionesController : ControllerBase
    {
        private readonly CotizacionService _cotizacionService;

        public CotizacionesController(CotizacionService cotizacionService)
        {
            _cotizacionService = cotizacionService;
        }

        // GET: api/Cotizaciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cotizacion>>> GetCotizaciones()
        {
            var cotizaciones = await _cotizacionService.GetAllCotizaciones();
            return Ok(cotizaciones);
        }

        // GET: api/Cotizaciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cotizacion>> GetCotizacion(int id)
        {
            var cotizacion = await _cotizacionService.GetCotizacionById(id);
            if (cotizacion == null)
            {
                return NotFound();
            }
            return Ok(cotizacion);
        }

        // POST: api/Cotizaciones
        [HttpPost]
        public async Task<ActionResult<Cotizacion>> PostCotizacion(Cotizacion cotizacion)
        {
            var createdCotizacion = await _cotizacionService.CreateCotizacion(cotizacion);
            return CreatedAtAction(nameof(GetCotizacion), new { id = createdCotizacion.Id }, createdCotizacion);
        }

        // PUT: api/Cotizaciones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCotizacion(int id, Cotizacion cotizacion)
        {
            if (id != cotizacion.Id)
            {
                return BadRequest();
            }
            var updatedCotizacion = await _cotizacionService.UpdateCotizacion(id, cotizacion);
            if (updatedCotizacion == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE: api/Cotizaciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCotizacion(int id)
        {
            var deletedCotizacion = await _cotizacionService.DeleteCotizacion(id);
            if (deletedCotizacion == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
