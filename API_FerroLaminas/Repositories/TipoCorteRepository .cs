using System.Linq;
using API_FerroLaminas.Data;
using API_FerroLaminas.Models;

namespace API_FerroLaminas.Repositories
{
    public class TipoCorteRepository: ITipoCorteRepository
    {
        private readonly ApplicationDbContext _context;

        public TipoCorteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TipoCorte> GetTiposCorte()
        {
            return _context.TiposCorte.ToList();
        }

        public TipoCorte GetTipoCorteById(int id)
        {
            return _context.TiposCorte.FirstOrDefault(tc => tc.Id == id);
        }

        public void CreateTipoCorte(TipoCorte tipoCorte)
        {
            _context.TiposCorte.Add(tipoCorte);
            _context.SaveChanges();
        }

        public void UpdateTipoCorte(TipoCorte tipoCorte)
        {
            _context.TiposCorte.Update(tipoCorte);
            _context.SaveChanges();
        }

        public void DeleteTipoCorte(int id)
        {
            var tipoCorte = _context.TiposCorte.Find(id);
            if (tipoCorte != null)
            {
                _context.TiposCorte.Remove(tipoCorte);
                _context.SaveChanges();
            }
        }
    }
}
