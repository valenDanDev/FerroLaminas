using API_FerroLaminas.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_FerroLaminas.Repositories
{
    public interface ICotizacionRepository
    {
        Task<IEnumerable<Cotizacion>> GetAllCotizaciones();
        Cotizacion GetCotizacionById(int id);
        Task<Cotizacion> CreateCotizacion(Cotizacion cotizacion);
        Task<Cotizacion> UpdateCotizacion(int id, Cotizacion cotizacion);
        Task<Cotizacion> DeleteCotizacion(int id);
    }
}
