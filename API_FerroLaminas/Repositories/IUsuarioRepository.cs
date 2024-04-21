using API_FerroLaminas.Models;

namespace API_FerroLaminas.Repositories
{
    public interface IUsuarioRepository
    {
        Usuario GetUsuarioByEmail(string email);
        Usuario GetUsuarioById(int userId);
        void CreateUsuario(Usuario usuario);
    }
}
