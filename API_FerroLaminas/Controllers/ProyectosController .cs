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
    public class ProyectoController : ControllerBase
    {
        private readonly IProyectoService _proyectoService;

        public ProyectoController(IProyectoService proyectoService)
        {
            _proyectoService = proyectoService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<IEnumerable<ProyectoDTO>>>> GetAllProyectos()
        {
            var response = await _proyectoService.GetAllProyectos();
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<ProyectoDTO>>> GetProyectoById(int id)
        {
            var response = await _proyectoService.GetProyectoById(id);
            if (!response.Success)
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<ProyectoDTO>>> CreateProyecto(ProyectoDTO proyectoDTO)
        {
            var response = await _proyectoService.CreateProyecto(proyectoDTO);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return CreatedAtAction(nameof(GetProyectoById), new { id = response.Data.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<ProyectoDTO>>> UpdateProyecto(int id, ProyectoDTO proyectoDTO)
        {
            var response = await _proyectoService.UpdateProyecto(id, proyectoDTO);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<ProyectoDTO>>> DeleteProyecto(int id)
        {
            var response = await _proyectoService.DeleteProyecto(id);
            if (!response.Success)
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }
    }
}
