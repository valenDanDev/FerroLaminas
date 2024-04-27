using API_FerroLaminas.Models;

namespace API_FerroLaminas.Repositories
{
    public interface IServicioRepository
    {
        IEnumerable<Servicio> GetServicios();
        Task<Servicio> GetServicioById(int id);
        void CreateServicio(Servicio servicio);
        Task<Servicio> UpdateServicio(int id, Servicio proyecto);
        void DeleteServicio(int id);
    }
}
