using System.Linq;
using API_FerroLaminas.Data;
using API_FerroLaminas.Models;

namespace API_FerroLaminas.Repositories
{
    public class UbicacionRepository : IUbicacionRepository
    {
        private readonly ApplicationDbContext _context;

        public UbicacionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Ubicacion> GetUbicaciones()
        {
            return _context.Ubicaciones.ToList();
        }
    }
}
