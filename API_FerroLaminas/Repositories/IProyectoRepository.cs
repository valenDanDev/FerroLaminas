using API_FerroLaminas.Models;

namespace API_FerroLaminas.Repositories
{
    public interface IProyectoRepository
    {
        Task<IEnumerable<Proyecto>> GetAllProyectos();
        Task<Proyecto> GetProyectoById(int id);
        Task<Proyecto> CreateProyecto(Proyecto proyecto);
        Task<Proyecto> UpdateProyecto(int id, Proyecto proyecto);
        Task<Proyecto> DeleteProyecto(int id);
    }
}
