using API_FerroLaminas.Models;
using API_FerroLaminas.Data;
using System;
using System.Linq;

namespace API_FerroLaminas.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Usuario GetUsuarioByEmail(string email)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Email == email);
        }

        public void CreateUsuario(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException(nameof(usuario));
            }

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }
    }
}
