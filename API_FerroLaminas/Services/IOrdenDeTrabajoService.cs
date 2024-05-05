using System.Collections.Generic;
using System.Threading.Tasks;
using API_FerroLaminas.DTO;
using API_FerroLaminas.DTOs;
using API_FerroLaminas.Models;

namespace API_FerroLaminas.Services
{
    public interface IOrdenDeTrabajoService
    {
        Task<ServiceResponse<IEnumerable<OrdenDeTrabajoDTO>>> GetAllOrdenesDeTrabajo();
        Task<ServiceResponse<OrdenDeTrabajoDTO>> GetOrdenDeTrabajoById(int id);
        Task<ServiceResponse<OrdenDeTrabajoDTO>> CreateOrdenDeTrabajo(OrdenDeTrabajoDTO ordenDeTrabajoDTO);
        Task<ServiceResponse<OrdenDeTrabajoDTO>> UpdateOrdenDeTrabajo(int id, OrdenDeTrabajoDTO ordenDeTrabajoDTO);
        Task<ServiceResponse<OrdenDeTrabajoDTO>> DeleteOrdenDeTrabajo(int id);

        Task<ServiceResponse<IEnumerable<OrdenDeTrabajoDTO>>> OrdenesTrabajoPendientes();
    }
}
