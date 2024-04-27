using API_FerroLaminas.DTO;
using API_FerroLaminas.Models;
using API_FerroLaminas.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_FerroLaminas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiciosController : ControllerBase
    {
        private readonly IServicioService _servicioService;

        public ServiciosController(IServicioService servicioService)
        {
            _servicioService = servicioService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ServicioDTO>> GetServicios()
        {
            try
            {
                var servicios = _servicioService.GetAllServicios();
                var serviciosDTO = servicios.Select(servicio => new ServicioDTO
                {
                    Id = servicio.Id,
                    Nombre = servicio.Nombre,
                    PrecioPorKilo = servicio.PrecioPorKilo,
                    Descripcion = servicio.Descripcion
                }).ToList();

                return Ok(serviciosDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error al obtener los servicios: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<ServicioDTO>>> GetServicio(int id)
        {
            var response = await _servicioService.GetServicioById(id);
            if (!response.Success)
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }

        [HttpPost]
        public ActionResult<ServiceResponse<ServicioDTO>> CreateServicio(ServicioDTO servicioDTO)
        {
            var response = _servicioService.CreateServicio(servicioDTO);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return CreatedAtAction(nameof(GetServicio), new { id = response.Data.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<ServicioDTO>>> UpdateServicio(int id, ServicioDTO proyectoDTO)
        {
            var response = await _servicioService.UpdateServicio(id, proyectoDTO);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public ActionResult<ServiceResponse<string>> DeleteServicio(int id)
        {
            var response = _servicioService.DeleteServicio(id);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }
    }
}