using API_FerroLaminas.Models;

namespace API_FerroLaminas.Repositories
{
    public interface IUsuarioRepository
    {
        Usuario GetUsuarioByEmail(string email);
        void CreateUsuario(Usuario usuario);
    }
}
