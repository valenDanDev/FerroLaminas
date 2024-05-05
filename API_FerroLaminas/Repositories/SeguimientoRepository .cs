using API_FerroLaminas.Data;
using API_FerroLaminas.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_FerroLaminas.Repositories
{
    public class SeguimientoRepository : ISeguimientoRepository
    {
        private readonly ApplicationDbContext _context;

        public SeguimientoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Seguimiento>> GetAllSeguimientos()
        {
            return await _context.Seguimientos.ToListAsync();
        }

        public async Task<Seguimiento> GetSeguimientoById(int id)
        {
            return await _context.Seguimientos.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Seguimiento>> GetSeguimientosByOrdenDeTrabajoId(int ordenDeTrabajoId)
        {
            return await _context.Seguimientos.Where(s => s.OrdenDeTrabajoId == ordenDeTrabajoId).ToListAsync();
        }

        public async Task<Seguimiento> CreateSeguimiento(Seguimiento seguimiento)
        {
            _context.Seguimientos.Add(seguimiento);
            await _context.SaveChangesAsync();
            return seguimiento;
        }

        public async Task<Seguimiento> UpdateSeguimiento(int id, Seguimiento seguimiento)
        {
            var existingSeguimiento = await _context.Seguimientos.FindAsync(id);
            if (existingSeguimiento == null)
            {
                return null;
            }

            existingSeguimiento.Fecha = seguimiento.Fecha;
            existingSeguimiento.Avance = seguimiento.Avance;
            existingSeguimiento.Observaciones = seguimiento.Observaciones;

            _context.Seguimientos.Update(existingSeguimiento);
            await _context.SaveChangesAsync();
            return existingSeguimiento;
        }

        public async Task<Seguimiento> DeleteSeguimiento(int id)
        {
            var seguimiento = await _context.Seguimientos.FindAsync(id);
            if (seguimiento == null)
            {
                return null;
            }

            _context.Seguimientos.Remove(seguimiento);
            await _context.SaveChangesAsync();
            return seguimiento;
        }
    }
}
