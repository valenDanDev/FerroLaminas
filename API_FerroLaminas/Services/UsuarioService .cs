using API_FerroLaminas.Models;
using API_FerroLaminas.Repositories;
using API_FerroLaminas.DTO;

namespace API_FerroLaminas.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IRolRepository _rolRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository, IRolRepository rolRepository)
        {
            _usuarioRepository = usuarioRepository;
            _rolRepository = rolRepository;
        }

        public ServiceResponse<object> ValidarYCrearUsuario(LoginRequestDTO loginRequest)
        {
            var existingUser = _usuarioRepository.GetUsuarioByEmail(loginRequest.Email);

            if (existingUser != null)
            {
                // El usuario ya existe, devuelve los datos del usuario existente y un mensaje informativo
                return new ServiceResponse<object>
                {
                    Success = true,
                    Message = "Usuario existente",
                    Data = new { Nombre = existingUser.Nombre, Email = existingUser.Email }
                };
            }
            else
            {
                // El usuario no existe, crea uno nuevo
                var rolCliente = _rolRepository.GetRolById(5); // Obtener el rol de cliente por defecto
                if (rolCliente == null)
                {
                    return new ServiceResponse<object>
                    {
                        Success = false,
                        Message = "Error al obtener el rol de cliente."
                    };
                }

                // Crear el usuario
                var newUser = new Usuario
                {
                    Nombre = loginRequest.Nombre,
                    Email = loginRequest.Email,
                    Rol = rolCliente
                };

                _usuarioRepository.CreateUsuario(newUser);

                // Devolver los datos del nuevo usuario creado y un mensaje informativo
                return new ServiceResponse<object>
                {
                    Success = true,
                    Message = "Usuario creado",
                    Data = new { Nombre = newUser.Nombre, Email = newUser.Email }
                };
            }
        }

    }
}
