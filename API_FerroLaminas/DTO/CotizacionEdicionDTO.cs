using API_FerroLaminas.Models;

namespace API_FerroLaminas.DTO
{
    public class CotizacionEdicionDTO
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

        // Properties for related entity information
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }

        public string Ubicacion { get; set; }
        public string DescripcionProyecto { get; set; }
        public decimal LargoProyecto { get; set; }
        public decimal AnchoProyecto { get; set; }
        public decimal Calibre { get; set; }
        public string TipoMaterial { get; set; }
        public string NombreServicio { get; set; }



        public CotizacionEdicionDTO(int id,int clienteId, int proyectoId, int materialId, int servicioId, decimal precioTotal, decimal pesoLamina, int usuarioId, decimal precioMaterial, decimal precioServicio, bool cotizacionFinalizada
            )
        {
            Id = id;
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
    }
}
