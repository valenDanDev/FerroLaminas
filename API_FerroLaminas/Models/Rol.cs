using System.ComponentModel.DataAnnotations;

namespace API_FerroLaminas.Models
{
    public class Rol
    {
        [Key] // Define IDRol as the primary key
        public int Id { get; set; } // Primary key
        public string Nombre { get; set; }

        // Colección de usuarios que tienen este rol
        public ICollection<Usuario> Usuarios { get; set; }
    }
}
