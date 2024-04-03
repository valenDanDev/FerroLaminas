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
    public class TipoCorteController : ControllerBase
    {
        private readonly ITipoCorteService _tipoCorteService;

        public TipoCorteController(ITipoCorteService tipoCorteService)
        {
            _tipoCorteService = tipoCorteService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TipoCorteDTO>> GetTiposCorte()
        {
            try
            {
                var tiposCorte = _tipoCorteService.GetAllTiposCorte();
                return Ok(tiposCorte);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error al obtener los tipos de corte: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<TipoCorteDTO> GetTipoCorte(int id)
        {
            try
            {
                var tipoCorte = _tipoCorteService.GetTipoCorteById(id);
                if (tipoCorte == null)
                {
                    return NotFound();
                }
                return Ok(tipoCorte);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al obtener el tipo de corte: " + ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<ServiceResponse<TipoCorteDTO>> CreateTipoCorte(TipoCorteDTO tipoCorteDTO)
        {
            var response = _tipoCorteService.CreateTipoCorte(tipoCorteDTO);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return CreatedAtAction(nameof(GetTipoCorte), new { id = response.Data.Id }, response);
        }

        [HttpPut("{id}")]
        public ActionResult<ServiceResponse<TipoCorteDTO>> UpdateTipoCorte(int id, TipoCorteDTO tipoCorteDTO)
        {
            var response = _tipoCorteService.UpdateTipoCorte(id, tipoCorteDTO);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public ActionResult<ServiceResponse<string>> DeleteTipoCorte(int id)
        {
            var response = _tipoCorteService.DeleteTipoCorte(id);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }
    }
}
