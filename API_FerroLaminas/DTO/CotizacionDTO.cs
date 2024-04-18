using API_FerroLaminas.Models;

namespace API_FerroLaminas.DTO
{
    public class CotizacionDTO
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int ProyectoId { get; set; }
        public int MaterialId { get; set; }
        public int ServicioId { get; set; }
        public decimal PrecioTotal { get; set; }
        public decimal PesoLamina { get; set; }
        public int UsuarioId { get; set; }

        // Agregar propiedades adicionales si es necesario

        // Constructor para inicializar el DTO a partir de una entidad Cotizacion
        public CotizacionDTO(Cotizacion cotizacion)
        {
            Id = cotizacion.Id;
            ClienteId = cotizacion.ClienteId;
            ProyectoId = cotizacion.ProyectoId;
            MaterialId = cotizacion.MaterialId;
            ServicioId = cotizacion.ServicioId;
            PrecioTotal = cotizacion.PrecioTotal;
            PesoLamina = cotizacion.PesoLamina;
            UsuarioId = cotizacion.UsuarioId;

            // Si es necesario mapear más propiedades, agrégalas aquí
        }
    }
}
