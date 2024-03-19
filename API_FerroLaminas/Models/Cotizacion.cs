namespace API_FerroLaminas.Models
{
    public class Cotizacion
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int ProyectoId { get; set; }
        public int MaterialId { get; set; }
        public int Calibre { get; set; }
        public decimal PrecioTotal { get; set; }

    }
}
