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

        public decimal PrecioMaterial { get; set; }
        public decimal PrecioServicio { get; set; }
        public int UsuarioId { get; set; }

        public bool CotizacionFinalizada { get; set; }

        public CotizacionDTO(int clienteId, int proyectoId, int materialId, int servicioId, decimal precioTotal, decimal pesoLamina, int usuarioId,decimal precioMaterial,decimal precioServicio,bool cotizacionFinalizada
            )
        {
            ClienteId = clienteId;
            ProyectoId = proyectoId;
            MaterialId = materialId;
            ServicioId = servicioId;
            PrecioTotal = precioTotal;
            PesoLamina = pesoLamina;
            UsuarioId = usuarioId;
            PrecioMaterial = precioMaterial;
            PrecioServicio = precioServicio;
            CotizacionFinalizada = cotizacionFinalizada;
        }

        public CotizacionDTO() { }
    }
}
