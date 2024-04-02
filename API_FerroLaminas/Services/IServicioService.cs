using API_FerroLaminas.DTO;
using API_FerroLaminas.Models;

namespace API_FerroLaminas.Services
{
    public interface IServicioService
    {
        IEnumerable<Servicio> GetAllServicios();
        Servicio GetServicioById(int id);
        ServiceResponse<ServicioDTO> CreateServicio(ServicioDTO servicioDTO);
        ServiceResponse<ServicioDTO> UpdateServicio(int id, ServicioDTO servicioDTO);
        ServiceResponse<string> DeleteServicio(int id);
    }
}
