public class OrdenDeTrabajo
{
    public int Id { get; set; }
    public int CotizacionId { get; set; }
    public int OperarioId { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public string Estado { get; set; }
}
