using Microsoft.AspNetCore.Mvc;
using API_FerroLaminas.DTOs;
using API_FerroLaminas.Services;

namespace API_FerroLaminas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalibreController: ControllerBase
    {
        private readonly ICalibreService _ubicacionesService;

        public CalibreController(ICalibreService ubicacionesService)
        {
            _ubicacionesService = ubicacionesService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CalibreDTO>> GetCalibresByMaterialId(int id)
        {
            try
            {
                var ubicaciones = _ubicacionesService.GetCalibresByMaterialId(id);
                return Ok(ubicaciones);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error al obtener las ubicaciones: " + ex.Message);
            }
        }
    }
}
