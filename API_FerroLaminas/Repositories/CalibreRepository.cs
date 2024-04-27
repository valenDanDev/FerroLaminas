using API_FerroLaminas.Data;
using API_FerroLaminas.Models;
using System.Linq;

namespace API_FerroLaminas.Repositories
{
    public class CalibreRepository: ICalibreRepository
    {
        private readonly ApplicationDbContext _context;

        public CalibreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Calibre> GetCalibresByMaterialId(int id)
        {
            // Use ToList() to convert IQueryable to a List<Calibre>
            var calibres = _context.Calibres.Where(c => c.MaterialId == id).ToList();
            return calibres;
        }

    }
}
