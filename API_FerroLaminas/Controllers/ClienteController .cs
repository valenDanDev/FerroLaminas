using API_FerroLaminas.DTO;
using API_FerroLaminas.Models;
using API_FerroLaminas.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_FerroLaminas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService ?? throw new ArgumentNullException(nameof(clienteService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> GetAllClientes()
        {
            var response = await _clienteService.GetAllClientes();
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            if (response.Data == null || !response.Data.Any())
            {
                return NotFound("Sin clientes en el momento");
            }
            return Ok(response.Data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDTO>> GetClienteById(int id)
        {
            var response = await _clienteService.GetClienteById(id);
            if (!response.Success)
            {
                return NotFound(response.Message);
            }
            return Ok(response.Data);
        }

        [HttpPost]
        public async Task<ActionResult<ClienteDTO>> CreateCliente([FromBody] ClienteDTO clienteDTO)
        {
            var cliente = new Cliente
            {
                cedula = clienteDTO.Cedula,
                Nombre = clienteDTO.Nombre,
                Telefono = clienteDTO.Telefono,
                Direccion = clienteDTO.Direccion,
                Email = clienteDTO.Email,
                UbicacionId = clienteDTO.UbicacionId
            };

            var response = await _clienteService.CreateCliente(cliente);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            var createdClienteDTO = new ClienteDTO
            {
                Cedula = response.Data.Cedula,
                Nombre = response.Data.Nombre,
                Telefono = response.Data.Telefono,
                Direccion = response.Data.Direccion,
                Email = response.Data.Email,
                UbicacionId = response.Data.UbicacionId
            };

            return CreatedAtAction(nameof(GetClienteById), new { id = createdClienteDTO.Cedula }, createdClienteDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ClienteDTO>> UpdateCliente(int id, [FromBody] ClienteDTO clienteDTO)
        {
            var cliente = new Cliente
            {
                cedula = clienteDTO.Cedula,
                Nombre = clienteDTO.Nombre,
                Telefono = clienteDTO.Telefono,
                Direccion = clienteDTO.Direccion,
                Email = clienteDTO.Email,
                UbicacionId = clienteDTO.UbicacionId
            };

            var response = await _clienteService.UpdateCliente(id, cliente);
            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            var updatedClienteDTO = new ClienteDTO
            {
                Cedula = response.Data.Cedula,
                Nombre = response.Data.Nombre,
                Telefono = response.Data.Telefono,
                Direccion = response.Data.Direccion,
                Email = response.Data.Email,
                UbicacionId = response.Data.UbicacionId
            };

            return Ok(updatedClienteDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ClienteDTO>> DeleteCliente(int id)
        {
            var response = await _clienteService.DeleteCliente(id);
            if (!response.Success)
            {
                return NotFound(response.Message);
            }
            return Ok(response.Data);
        }
    }
}
