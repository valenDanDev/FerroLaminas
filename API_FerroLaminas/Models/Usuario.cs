namespace API_FerroLaminas.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public int RolId { get; set; } // Corrección
        public Rol Rol { get; set; }
    }
}
