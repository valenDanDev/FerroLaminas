using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using API_FerroLaminas.Models;
using API_FerroLaminas.Services;
using API_FerroLaminas.DTO;

namespace API_FerroLaminas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CotizacionesController : ControllerBase
    {

        private readonly ICotizacionService _cotizacionService;

        public CotizacionesController(ICotizacionService cotizacionService)
        {
            _cotizacionService = cotizacionService;
        }

        // GET: api/Cotizaciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CotizacionDTO>>> GetCotizaciones()
        {
            
            var response = await _cotizacionService.GetAllCotizaciones();
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            if (response.Data == null || !response.Data.Any())
            {
                return NotFound("Sin cotizaciones en el momento"); // Sin contenido para devolver
            }

            return Ok(response.Data);
        }

        // GET: api/Cotizaciones/5
        [HttpGet("{id}")]
        public ActionResult<CotizacionDTO> GetCotizacion(int id)
        {
            var response = _cotizacionService.GetCotizacionById(id);
            if (!response.Success)
            {
                return NotFound(response.Message);
            }
            return Ok(response.Data);
        }

        // POST: api/Cotizaciones
        [HttpPost]
        public async Task<ActionResult<CotizacionDTO>> PostCotizacion(CotizacionDTO cotizacionDTO)
        {
            // Mapear el DTO a un objeto Cotizacion
            var cotizacion = new Cotizacion
            {
                ClienteId = cotizacionDTO.ClienteId,
                ProyectoId = cotizacionDTO.ProyectoId,
                MaterialId = cotizacionDTO.MaterialId,
                ServicioId = cotizacionDTO.ServicioId,
                PrecioTotal = cotizacionDTO.PrecioTotal,
                PesoLamina = cotizacionDTO.PesoLamina,
                UsuarioId = cotizacionDTO.UsuarioId
            };

            var response = await _cotizacionService.CreateCotizacion(cotizacion);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return CreatedAtAction(nameof(GetCotizacion), new { id = response.Data.Id }, response.Data);
        }


        // PUT: api/Cotizaciones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCotizacion(int id, CotizacionDTO updateCotizacionDTO)
        {
            var cotizacion = new Cotizacion
            {
                Id = id,
                ClienteId = updateCotizacionDTO.ClienteId,
                ProyectoId = updateCotizacionDTO.ProyectoId,
                MaterialId = updateCotizacionDTO.MaterialId,
                ServicioId = updateCotizacionDTO.ServicioId,
                PrecioTotal = updateCotizacionDTO.PrecioTotal,
                PesoLamina = updateCotizacionDTO.PesoLamina,
                UsuarioId = updateCotizacionDTO.UsuarioId
            };

            var response = await _cotizacionService.UpdateCotizacion(id, cotizacion);
            if (!response.Success)
            {
                return NotFound(response.Message);
            }
            return NoContent();
        }


        // DELETE: api/Cotizaciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCotizacion(int id)
        {
            var response = await _cotizacionService.DeleteCotizacion(id);
            if (!response.Success)
            {
                return NotFound(response.Message);
            }
            return NoContent();
        }
    }
}
