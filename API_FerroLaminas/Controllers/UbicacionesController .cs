using API_FerroLaminas.DTO;
using API_FerroLaminas.Models;
using API_FerroLaminas.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace API_FerroLaminas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UbicacionesController : ControllerBase
    {
        private readonly IUbicacionesService _ubicacionesService;

        public UbicacionesController(IUbicacionesService ubicacionesService)
        {
            _ubicacionesService = ubicacionesService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UbicacionDTO>> GetUbicaciones()
        {
            try
            {
                var ubicaciones = _ubicacionesService.GetAllLocations();
                return Ok(ubicaciones);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error al obtener las ubicaciones: " + ex.Message);
            }
        }

    }
}
