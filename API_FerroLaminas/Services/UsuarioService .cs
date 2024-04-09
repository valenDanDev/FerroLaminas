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
            var userDataResponse = ObtenerDatosUsuario(loginRequest.Email);

            if (!userDataResponse.Success)
            {
                return CrearNuevoUsuario(loginRequest);
            }
            else
            {
                return UsuarioExistente(userDataResponse.Data);
            }
        }

        private ServiceResponse<Usuario> ObtenerDatosUsuario(string email)
        {
            var existingUser = _usuarioRepository.GetUsuarioByEmail(email);

            if (existingUser != null)
            {
                return new ServiceResponse<Usuario>
                {
                    Success = true,
                    Data = existingUser
                };
            }
            else
            {
                return new ServiceResponse<Usuario>
                {
                    Success = false,
                    Message = "Usuario no encontrado."
                };
            }
        }

        private ServiceResponse<object> UsuarioExistente(Usuario existingUser)
        {
            return new ServiceResponse<object>
            {
                Success = true,
                Message = "Usuario existente",
                Data = new { Nombre = existingUser.Nombre, Email = existingUser.Email,IdUsuario=existingUser.Id }
            };
        }

        private ServiceResponse<object> CrearNuevoUsuario(LoginRequestDTO loginRequest)
        {
            var rolCliente = _rolRepository.GetRolById(5); // Obtener el rol de cliente por defecto
            if (rolCliente == null)
            {
                return new ServiceResponse<object>
                {
                    Success = false,
                    Message = "Error al obtener el rol de cliente."
                };
            }

            var newUser = new Usuario
            {
                Nombre = loginRequest.Nombre,
                Email = loginRequest.Email,
                Rol = rolCliente
            };

            _usuarioRepository.CreateUsuario(newUser);

            return new ServiceResponse<object>
            {
                Success = true,
                Message = "Usuario creado",
                Data = new { Nombre = newUser.Nombre, Email = newUser.Email, IdUsuario = newUser.Id }
            };
        }

        public ServiceResponse<object> GetRol(int userId)
        {
            var userDataResponse = ObtenerDatosUsuario(userId); // Cambiar el argumento a userId

            if (!userDataResponse.Success)
            {
                return new ServiceResponse<object>
                {
                    Success = false,
                    Message = userDataResponse.Message
                };
            }

            var user = userDataResponse.Data;
            var rol = _rolRepository.GetRolById(user.RolId);
            if (rol == null)
            {
                return new ServiceResponse<object>
                {
                    Success = false,
                    Message = "Rol no encontrado."
                };
            }

            return new ServiceResponse<object>
            {
                Success = true,
                Data = new RolDTO { Nombre = rol.Nombre }
            };
        }

        private ServiceResponse<Usuario> ObtenerDatosUsuario(int userId) // Cambiar el tipo de argumento a int
        {
            var user = _usuarioRepository.GetUsuarioById(userId);
            if (user == null)
            {
                return new ServiceResponse<Usuario>
                {
                    Success = false,
                    Message = "Usuario no encontrado."
                };
            }

            return new ServiceResponse<Usuario>
            {
                Success = true,
                Data = user
            };
        }
        public ServiceResponse<object> GetUsuarioById(int userId)
        {
            var userDataResponse = ObtenerDatosUsuario(userId); // Obtener datos del usuario

            if (!userDataResponse.Success)
            {
                return new ServiceResponse<object>
                {
                    Success = false,
                    Message = userDataResponse.Message
                };
            }

            var user = userDataResponse.Data;

            // Devolver los datos del usuario
            return new ServiceResponse<object>
            {
                Success = true,
                Data = new { Nombre = user.Nombre, Email = user.Email }
            };
        }
    }
}
