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
        public ActionResult<ServicioDTO> GetServicio(int id)
        {
            try
            {
                var servicio = _servicioService.GetServicioById(id);
                if (servicio == null)
                {
                    return NotFound();
                }

                var servicioDTO = new ServicioDTO
                {
                    Id = servicio.Id,
                    Nombre = servicio.Nombre,
                    PrecioPorKilo = servicio.PrecioPorKilo,
                    Descripcion = servicio.Descripcion
                };

                return Ok(servicioDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al obtener el servicio: " + ex.Message);
            }
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
        public ActionResult<ServiceResponse<ServicioDTO>> UpdateServicio(int id, ServicioDTO servicioDTO)
        {
            var response = _servicioService.UpdateServicio(id, servicioDTO);
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