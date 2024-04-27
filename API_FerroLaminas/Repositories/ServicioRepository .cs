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

        public async Task<Servicio> GetServicioById(int id)
        {
            return _context.Servicios.Find(id);
        }

        public async Task<Material> GetMaterialById(int id)
        {
            return _context.Materiales.Find(id);
        }

        public void CreateServicio(Servicio servicio)
        {
            _context.Servicios.Add(servicio);
            _context.SaveChanges();
        }


        public async Task<Servicio> UpdateServicio(int id, Servicio proyecto)
        {
            var existingProyecto = await _context.Servicios.FindAsync(id);
            if (existingProyecto == null)
            {
                return null; // Proyecto no encontrado
            }

            existingProyecto.Descripcion = proyecto.Descripcion;
            existingProyecto.Nombre = proyecto.Nombre;
            existingProyecto.PrecioPorKilo = proyecto.PrecioPorKilo;

            _context.Servicios.Update(existingProyecto);
            await _context.SaveChangesAsync();

            return existingProyecto;
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