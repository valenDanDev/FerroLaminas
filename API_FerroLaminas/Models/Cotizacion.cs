namespace API_FerroLaminas.Models
{
    public class Cotizacion
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int ProyectoId { get; set; }
        public int MaterialId { get; set; }
        public int ServicioId { get; set; } 
        public decimal PrecioTotal { get; set; }
        public decimal PesoLamina { get; set; }

        public decimal precioMaterial { get; set; }
        public decimal precioServicio { get; set; }

        public bool CotizacionFinalizada { get; set; }

        public Cliente Cliente { get; set; }
        public Material Material { get; set; }
        public Servicio Servicio { get; set; }
        public OrdenDeTrabajo OrdenDeTrabajo { get; set; }
        public Proyecto Proyecto { get; set; } 

        // Relación con Usuarios
        public int UsuarioId { get; set; } // Clave foránea de Usuario
        public Usuario Usuario { get; set; }

    }
}
