namespace API_FerroLaminas.Models
{
    public class OrdenDeTrabajo
    {
        public int Id { get; set; }
        public int CotizacionId { get; set; }
        public string OperarioId { get; set; }
        public string nombreOperario { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int EstadoId { get; set; }
        public EstadoOrdenTrabajo Estado { get; set; }
        // Relación con Cotizacion
        public Cotizacion Cotizacion { get; set; } // Agregar esta propiedad

        // Relación con Seguimientos
        public ICollection<Seguimiento> Seguimientos { get; set; }
    }
}
