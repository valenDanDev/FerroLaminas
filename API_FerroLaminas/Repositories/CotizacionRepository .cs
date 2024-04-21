using API_FerroLaminas.Data;
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
            return _context.Cotizaciones.FirstOrDefault(c => c.Id == id);
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
