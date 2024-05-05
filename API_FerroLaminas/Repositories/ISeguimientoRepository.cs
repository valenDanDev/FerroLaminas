using API_FerroLaminas.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_FerroLaminas.Repositories
{
    public interface ISeguimientoRepository
    {
        Task<IEnumerable<Seguimiento>> GetAllSeguimientos();
        Task<Seguimiento> GetSeguimientoById(int id);
        Task<IEnumerable<Seguimiento>> GetSeguimientosByOrdenDeTrabajoId(int ordenDeTrabajoId);
        Task<Seguimiento> CreateSeguimiento(Seguimiento seguimiento);
        Task<Seguimiento> UpdateSeguimiento(int id, Seguimiento seguimiento);
        Task<Seguimiento> DeleteSeguimiento(int id);
    }
}
