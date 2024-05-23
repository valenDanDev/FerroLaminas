using API_FerroLaminas.Data;
using API_FerroLaminas.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_FerroLaminas.Repositories
{
    public class EstadoOrdenTrabajoRepository
    {
        private readonly ApplicationDbContext _context;

        public EstadoOrdenTrabajoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<EstadoOrdenTrabajo>> GetAllEstadosAsync()
        {
            return await _context.EstadosOrdenTrabajo.ToListAsync();
        }
    }
}
