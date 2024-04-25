using API_FerroLaminas.Models;

namespace API_FerroLaminas.Repositories
{
    public interface IUbicacionRepository
    {
        IEnumerable<Ubicacion> GetUbicaciones();
    }
}