using System.Text.Json.Serialization;

namespace API_FerroLaminas.Models
{
    public class Material
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public decimal PrecioPorKilo { get; set; } // Precio por kilogramo
        public decimal StockKilos { get; set; } // Stock en kilogramos
        public string Descripcion { get; set; }
        public decimal Densidad { get; set; }

        // Relación con Calibre
        public ICollection<Calibre> Calibres { get; set; }

        // Relación con Cotizaciones
        public ICollection<Cotizacion> Cotizaciones { get; set; }

    }
}
