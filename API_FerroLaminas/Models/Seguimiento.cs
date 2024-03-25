namespace API_FerroLaminas.Models
{
    public class Seguimiento
    {
        public int Id { get; set; }
        public int OrdenDeTrabajoId { get; set; } // FK de OrdenDeTrabajo

        // Otras propiedades de seguimiento
        public DateTime Fecha { get; set; }
        public decimal Avance { get; set; }
        public string Observaciones { get; set; }

        // Propiedad de navegación hacia la orden de trabajo
        public OrdenDeTrabajo OrdenDeTrabajo { get; set; }
    }
}
