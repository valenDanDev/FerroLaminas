using API_FerroLaminas.Repositories;
using API_FerroLaminas.Models;
namespace API_FerroLaminas.Services
{
    public class CotizacionService
    {
        private readonly ICotizacionRepository _cotizacionRepository;

        public CotizacionService(ICotizacionRepository cotizacionRepository)
        {
            _cotizacionRepository = cotizacionRepository;
        }

        public async Task<IEnumerable<Cotizacion>> GetAllCotizaciones()
        {
            return await _cotizacionRepository.GetAll();
        }

        public async Task<Cotizacion> GetCotizacionById(int id)
        {
            return await _cotizacionRepository.GetById(id);
        }

        public async Task<Cotizacion> CreateCotizacion(Cotizacion cotizacion)
        {
            return await _cotizacionRepository.Add(cotizacion);
        }

        public async Task<Cotizacion> UpdateCotizacion(int id, Cotizacion cotizacion)
        {
            var existingCotizacion = await _cotizacionRepository.GetById(id);
            if (existingCotizacion == null)
            {
                return null;
            }
            cotizacion.Id = id; // Ensure the ID is preserved
            return await _cotizacionRepository.Update(id, cotizacion);
        }

        public async Task<Cotizacion> DeleteCotizacion(int id)
        {
            return await _cotizacionRepository.Delete(id);
        }
    }
}
