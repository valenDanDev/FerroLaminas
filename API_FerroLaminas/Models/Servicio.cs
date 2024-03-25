namespace API_FerroLaminas.Models
{
    public class Servicio
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal PrecioPorKilo { get; set; } // Precio por kilogramo
        public string Descripcion { get; set; }

        // Relación con Cotizaciones
        public ICollection<Cotizacion> Cotizaciones { get; set; }
    }
}
