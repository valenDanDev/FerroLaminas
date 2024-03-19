using System.Collections.Generic;
using System.Threading.Tasks;
using API_FerroLaminas.Models;


namespace API_FerroLaminas.Repositories
{
    public interface ICotizacionRepository
    {
        Task<IEnumerable<Cotizacion>> GetAll();
        Task<Cotizacion> GetById(int id);
        Task<Cotizacion> Add(Cotizacion cotizacion);
        Task<Cotizacion> Update(int id, Cotizacion cotizacion);
        Task<Cotizacion> Delete(int id);
    }
}
