using API_FerroLaminas.DTO;
using API_FerroLaminas.Models;

namespace API_FerroLaminas.Services
{
    public interface IProyectoService
    {
        Task<ServiceResponse<IEnumerable<ProyectoDTO>>> GetAllProyectos();
        Task<ServiceResponse<ProyectoDTO>> GetProyectoById(int id);
        Task<ServiceResponse<ProyectoDTO>> CreateProyecto(ProyectoDTO proyectoDTO);
        Task<ServiceResponse<ProyectoDTO>> UpdateProyecto(int id, ProyectoDTO proyectoDTO);
        Task<ServiceResponse<ProyectoDTO>> DeleteProyecto(int id);
    }
}
