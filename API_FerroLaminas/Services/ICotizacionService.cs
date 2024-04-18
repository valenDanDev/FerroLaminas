using API_FerroLaminas.DTO;
using API_FerroLaminas.Models;
using System.Threading.Tasks;

namespace API_FerroLaminas.Services
{
    public interface ICotizacionService
    {
        Task<ServiceResponse<IEnumerable<CotizacionDTO>>> GetAllCotizaciones();
        ServiceResponse<CotizacionDTO> GetCotizacionById(int id);
        Task<ServiceResponse<CotizacionDTO>> CreateCotizacion(Cotizacion cotizacion);
        Task<ServiceResponse<CotizacionDTO>> UpdateCotizacion(int id, Cotizacion cotizacion);
        Task<ServiceResponse<CotizacionDTO>> DeleteCotizacion(int id);
    }
}
