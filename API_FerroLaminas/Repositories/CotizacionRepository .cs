using API_FerroLaminas.Data;
using API_FerroLaminas.DTO;
using API_FerroLaminas.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_FerroLaminas.Repositories
{
    public class CotizacionRepository : ICotizacionRepository
    {
        private readonly ApplicationDbContext _context;

        public CotizacionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cotizacion>> GetAllCotizaciones()
        {
            return await _context.Cotizaciones.ToListAsync();
        }

        public Cotizacion GetCotizacionById(int id)
        {
            // Perform JOIN query with eager loading
            var cotizacionData = _context.Cotizaciones
                .Include(c => c.Cliente) // Include Cliente entity
                    .Include(c => c.Cliente.Ubicacion) // Include Ubicacion entity nested within Cliente
                .Include(c => c.Proyecto) // Include Proyecto entity
                .Include(c => c.Servicio) // Include Servicio entity
                .Include(c => c.Material) // Include Material entity
                .Where(c => c.Id == id)
                .FirstOrDefault();

            if (cotizacionData == null)
            {
                return null; // Handle case where cotisation isn't found
            }

            return cotizacionData;
        }

        public async Task<Cotizacion> CreateCotizacion(Cotizacion cotizacion)
        {
            _context.Cotizaciones.Add(cotizacion);
            await _context.SaveChangesAsync();
            return cotizacion;
        }

        public async Task<Cotizacion> UpdateCotizacion(int id, Cotizacion cotizacion)
        {
            _context.Entry(cotizacion).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return cotizacion;
        }

        public async Task<Cotizacion> DeleteCotizacion(int id)
        {
            var cotizacion = await _context.Cotizaciones.FindAsync(id);
            if (cotizacion == null)
            {
                return null;
            }

            _context.Cotizaciones.Remove(cotizacion);
            await _context.SaveChangesAsync();
            return cotizacion;
        }

    }
}
