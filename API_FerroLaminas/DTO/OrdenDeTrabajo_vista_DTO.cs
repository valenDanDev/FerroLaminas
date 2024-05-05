namespace API_FerroLaminas.DTO
{
    public class OrdenDeTrabajo_vista_DTO
    {
        public int Id { get; set; }
        public int CotizacionId { get; set; }
        public string OperarioId { get; set; }
        public string NombreOperario { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int EstadoId { get; set; }
        public string? Estado { get; set; }
    }
}
