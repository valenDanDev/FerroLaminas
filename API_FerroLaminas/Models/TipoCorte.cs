namespace API_FerroLaminas.Models
{
    public class TipoCorte
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal PrecioPorKilo { get; set; } // Precio por kilogramo
        public string Descripcion { get; set; }

        // Relación con el servicio de corte
        public int ServicioId { get; set; }
        public Servicio Servicio { get; set; }
    }
}
