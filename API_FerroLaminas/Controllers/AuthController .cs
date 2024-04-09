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

    }
}
