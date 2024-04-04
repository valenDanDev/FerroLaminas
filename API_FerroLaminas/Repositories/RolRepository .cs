using API_FerroLaminas.Models;
using API_FerroLaminas.Data;
using System;
using System.Linq;

namespace API_FerroLaminas.Repositories
{
    public class RolRepository : IRolRepository
    {
        private readonly ApplicationDbContext _context;

        public RolRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Rol GetRolById(int id)
        {
            return _context.Roles.FirstOrDefault(r => r.Id == id);
        }
    }
}
