using API_FerroLaminas.DTO;
using API_FerroLaminas.Models;

namespace API_FerroLaminas.Services
{
    public interface IUsuarioService
    {
        ServiceResponse<object> ValidarYCrearUsuario(LoginRequestDTO loginRequest);
        ServiceResponse<object> GetRol(int userId);
        ServiceResponse<object> GetUsuarioById(int userId);
    }
}
