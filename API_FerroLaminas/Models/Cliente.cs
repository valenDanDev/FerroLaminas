namespace API_FerroLaminas.Models
{
    public class Cliente
    {
        public int cedula { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }

        // Relacionado con la ubicación del cliente
        public int UbicacionId { get; set; } // Id de la ubicación
        public Ubicacion Ubicacion { get; set; } // Propiedad de navegación hacia la ubicación

        // Relación con Cotizaciones
        public ICollection<Cotizacion> Cotizaciones { get; set; }

    }
}
