namespace API_FerroLaminas.DTO
{
    public class SeguimientoDTO
    {

        public int Id { get; set; }
        public int OrdenDeTrabajoId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Avance { get; set; }
        public string Observaciones { get; set; }
    }
}
