using API_FerroLaminas.Data;
using API_FerroLaminas.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_FerroLaminas.Repositories
{
    public class ProyectoRepository : IProyectoRepository
    {
        private readonly ApplicationDbContext _context;

        public ProyectoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Proyecto>> GetAllProyectos()
        {
            return await _context.Proyectos.ToListAsync();
        }

        public async Task<Proyecto> GetProyectoById(int id)
        {
            return await _context.Proyectos.FindAsync(id);
        }

        public async Task<Proyecto> CreateProyecto(Proyecto proyecto)
        {
            _context.Proyectos.Add(proyecto);
            await _context.SaveChangesAsync();
            return proyecto;
        }

        public async Task<Proyecto> UpdateProyecto(int id, Proyecto proyecto)
        {
            var existingProyecto = await _context.Proyectos.FindAsync(id);
            if (existingProyecto == null)
            {
                return null; // Proyecto no encontrado
            }

            existingProyecto.Descripcion = proyecto.Descripcion;
            existingProyecto.Largo = proyecto.Largo;
            existingProyecto.Ancho = proyecto.Ancho;

            _context.Proyectos.Update(existingProyecto);
            await _context.SaveChangesAsync();

            return existingProyecto;
        }

        public async Task<Proyecto> DeleteProyecto(int id)
        {
            var proyecto = await _context.Proyectos.FindAsync(id);
            if (proyecto == null)
            {
                return null; // Proyecto no encontrado
            }

            _context.Proyectos.Remove(proyecto);
            await _context.SaveChangesAsync();

            return proyecto;
        }
    }
}
