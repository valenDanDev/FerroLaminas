namespace API_FerroLaminas.Models
{
    public class Cotizacion
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int ProyectoId { get; set; }
        public int MaterialId { get; set; }
        public int ServicioId { get; set; } // Corrección
        public decimal PrecioTotal { get; set; }
        public decimal PesoLamina { get; set; }

        public Cliente Cliente { get; set; }
        public Material Material { get; set; }
        public Servicio Servicio { get; set; }
        public OrdenDeTrabajo OrdenDeTrabajo { get; set; }
        public Proyecto Proyecto { get; set; } // Corrección

        // Relación con Usuarios
        public int UsuarioId { get; set; } // Clave foránea de Usuario
        public Usuario Usuario { get; set; }

    }
}
