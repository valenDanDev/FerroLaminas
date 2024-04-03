namespace API_FerroLaminas.DTO
{
    public class TipoCorteDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal PrecioPorKilo { get; set; }
        public string Descripcion { get; set; }
        public int ServicioId { get; set; }
    }
}
