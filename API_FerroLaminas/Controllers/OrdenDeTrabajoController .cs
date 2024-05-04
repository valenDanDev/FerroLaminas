using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using API_FerroLaminas.DTO;
using API_FerroLaminas.Services;

namespace API_FerroLaminas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenDeTrabajoController : ControllerBase
    {
        private readonly IOrdenDeTrabajoService _ordenDeTrabajoService;

        public OrdenDeTrabajoController(IOrdenDeTrabajoService ordenDeTrabajoService)
        {
            _ordenDeTrabajoService = ordenDeTrabajoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _ordenDeTrabajoService.GetAllOrdenesDeTrabajo();
            if (response.Success)
            {
                return Ok(response.Data);
            }
            return BadRequest(response.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _ordenDeTrabajoService.GetOrdenDeTrabajoById(id);
            if (response.Success)
            {
                return Ok(response.Data);
            }
            return NotFound(response.Message);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrdenDeTrabajoDTO ordenDeTrabajoDTO)
        {
            var response = await _ordenDeTrabajoService.CreateOrdenDeTrabajo(ordenDeTrabajoDTO);
            if (response.Success)
            {
                return CreatedAtAction(nameof(GetById), new { id = response.Data.Id }, response.Data);
            }
            return BadRequest(response.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, OrdenDeTrabajoDTO ordenDeTrabajoDTO)
        {
            var response = await _ordenDeTrabajoService.UpdateOrdenDeTrabajo(id, ordenDeTrabajoDTO);
            if (response.Success)
            {
                return Ok(response.Data);
            }
            return BadRequest(response.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _ordenDeTrabajoService.DeleteOrdenDeTrabajo(id);
            if (response.Success)
            {
                return NoContent();
            }
            return NotFound(response.Message);
        }
    }
}
