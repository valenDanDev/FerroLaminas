using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_FerroLaminas.Data;
using API_FerroLaminas.Models;
using Microsoft.EntityFrameworkCore;

namespace API_FerroLaminas.Repositories
{
    public class OrdenDeTrabajoRepository : IOrdenDeTrabajoRepository
    {
        private readonly ApplicationDbContext _context;

        public OrdenDeTrabajoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrdenDeTrabajo>> GetAllOrdenesDeTrabajo()
        {
            return await _context.OrdenesDeTrabajo
                .Include(o => o.Estado) // Incluir el estado si es necesario
                .ToListAsync();
        }

        public async Task<OrdenDeTrabajo> GetOrdenDeTrabajoById(int id)
        {
            return await _context.OrdenesDeTrabajo
                .Include(o => o.Estado) // Incluir el estado si es necesario
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<OrdenDeTrabajo> CreateOrdenDeTrabajo(OrdenDeTrabajo ordenDeTrabajo)
        {
            _context.OrdenesDeTrabajo.Add(ordenDeTrabajo);
            await _context.SaveChangesAsync();
            return ordenDeTrabajo;
        }

        public async Task<OrdenDeTrabajo> UpdateOrdenDeTrabajo(int id, OrdenDeTrabajo ordenDeTrabajo)
        {
            var existingOrdenDeTrabajo = await _context.OrdenesDeTrabajo.FindAsync(id);
            if (existingOrdenDeTrabajo == null)
            {
                return null; // Orden de trabajo no encontrada
            }

            existingOrdenDeTrabajo.CotizacionId = ordenDeTrabajo.CotizacionId;
            existingOrdenDeTrabajo.OperarioId = ordenDeTrabajo.OperarioId;
            existingOrdenDeTrabajo.nombreOperario = ordenDeTrabajo.nombreOperario;
            existingOrdenDeTrabajo.FechaInicio = ordenDeTrabajo.FechaInicio;
            existingOrdenDeTrabajo.FechaFin = ordenDeTrabajo.FechaFin;
            existingOrdenDeTrabajo.EstadoId = ordenDeTrabajo.EstadoId;

            _context.OrdenesDeTrabajo.Update(existingOrdenDeTrabajo);
            await _context.SaveChangesAsync();

            return existingOrdenDeTrabajo;
        }

        public async Task<OrdenDeTrabajo> DeleteOrdenDeTrabajo(int id)
        {
            var ordenDeTrabajo = await _context.OrdenesDeTrabajo.FindAsync(id);
            if (ordenDeTrabajo == null)
            {
                return null; // Orden de trabajo no encontrada
            }

            _context.OrdenesDeTrabajo.Remove(ordenDeTrabajo);
            await _context.SaveChangesAsync();

            return ordenDeTrabajo;
        }
    }
}
