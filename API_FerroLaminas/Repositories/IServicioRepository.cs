using API_FerroLaminas.Models;

namespace API_FerroLaminas.Repositories
{
    public interface IServicioRepository
    {
        IEnumerable<Servicio> GetServicios();
        Servicio GetServicioById(int id);
        void CreateServicio(Servicio servicio);
        void UpdateServicio(Servicio servicio);
        void DeleteServicio(int id);
    }
}
