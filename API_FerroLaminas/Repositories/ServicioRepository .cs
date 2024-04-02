using API_FerroLaminas.Data;
using API_FerroLaminas.Models;

namespace API_FerroLaminas.Repositories
{
    public class ServicioRepository : IServicioRepository
    {
        private readonly ApplicationDbContext _context;

        public ServicioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Servicio> GetServicios()
        {
            return _context.Servicios;
        }

        public Servicio GetServicioById(int id)
        {
            return _context.Servicios.Find(id);
        }

        public void CreateServicio(Servicio servicio)
        {
            _context.Servicios.Add(servicio);
            _context.SaveChanges();
        }

        public void UpdateServicio(Servicio servicio)
        {
            _context.Servicios.Update(servicio);
            _context.SaveChanges();
        }

        public void DeleteServicio(int id)
        {
            var servicio = _context.Servicios.Find(id);
            if (servicio != null)
            {
                _context.Servicios.Remove(servicio);
                _context.SaveChanges();
            }
        }
    }
}