using API_FerroLaminas.Models;
using API_FerroLaminas.Repositories;
using API_FerroLaminas.DTO;
using System;
using System.Linq;


namespace API_FerroLaminas.Services
{
    public class UsuarioService: IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IRolRepository _rolRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository, IRolRepository rolRepository)
        {
            _usuarioRepository = usuarioRepository;
            _rolRepository = rolRepository;
        }

        public ServiceResponse<Usuario> ValidarYCrearUsuario(LoginRequestDTO loginRequest)
        {
            var existingUser = _usuarioRepository.GetUsuarioByEmail(loginRequest.Email);

            if (existingUser != null)
            {
                // El usuario ya existe, devuelve éxito
                return new ServiceResponse<Usuario>
                {
                    Success = true,
                    Data = existingUser
                };
            }
            else
            {
                // El usuario no existe, crea uno nuevo
                var rolCliente = _rolRepository.GetRolById(5); // Obtener el rol de cliente por defecto
                if (rolCliente == null)
                {
                    return new ServiceResponse<Usuario>
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

                return new ServiceResponse<Usuario>
                {
                    Success = true,
                    Data = newUser
                };
            }
        }
    }
}
