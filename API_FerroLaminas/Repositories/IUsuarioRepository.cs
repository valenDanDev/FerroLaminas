using API_FerroLaminas.Models;

namespace API_FerroLaminas.Repositories
{
    public interface IUsuarioRepository
    {
        Usuario GetUsuarioByEmail(string email);
        Usuario GetUsuarioById(int userId); // Nuevo método para obtener un usuario por su ID
        void CreateUsuario(Usuario usuario);
    }
}
