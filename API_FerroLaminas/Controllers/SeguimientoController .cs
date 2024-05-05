using API_FerroLaminas.DTO;
using API_FerroLaminas.Models;
using API_FerroLaminas.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_FerroLaminas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeguimientoController : ControllerBase
    {
        private readonly ISeguimientoService _seguimientoService;

        public SeguimientoController(ISeguimientoService seguimientoService)
        {
            _seguimientoService = seguimientoService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<IEnumerable<SeguimientoDTO>>>> GetAllSeguimientos()
        {
            var response = await _seguimientoService.GetAllSeguimientos();
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<SeguimientoDTO>>> GetSeguimientoById(int id)
        {
            var response = await _seguimientoService.GetSeguimientoById(id);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<SeguimientoDTO>>> CreateSeguimiento(SeguimientoDTO seguimientoDTO)
        {
            var response = await _seguimientoService.CreateSeguimiento(seguimientoDTO);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<SeguimientoDTO>>> UpdateSeguimiento(int id, SeguimientoDTO seguimientoDTO)
        {
            var response = await _seguimientoService.UpdateSeguimiento(id, seguimientoDTO);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<SeguimientoDTO>>> DeleteSeguimiento(int id)
        {
            var response = await _seguimientoService.DeleteSeguimiento(id);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
