using API_FerroLaminas.DTO;
using API_FerroLaminas.Models;
using API_FerroLaminas.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_FerroLaminas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public AuthController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("login")]
        public ActionResult<ServiceResponse<object>> Login(LoginRequestDTO loginRequest)
        {
            var response = _usuarioService.ValidarYCrearUsuario(loginRequest);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            if (response.Message != null)
            {
                return Ok(new { Message = response.Message, Data = response.Data });
            }

            return Ok(response.Data);
        }

        [HttpGet("rol/{userId}")]
        public ActionResult<ServiceResponse<object>> GetRol(int userId)
        {
            var response = _usuarioService.GetRol(userId);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Data);
        }

        [HttpGet("Operarios")]
        public async Task<IActionResult> ObtenerTodosOperarios()
        {
            try
            {
                var response = _usuarioService.ObtenerTodosOperarios();
                if (!response.Success)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

    }
}
