using API_FerroLaminas.DTO;
using API_FerroLaminas.DTOs;
using API_FerroLaminas.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_FerroLaminas.Services
{
    public interface ISeguimientoService
    {
        Task<ServiceResponse<IEnumerable<SeguimientoDTO>>> GetAllSeguimientos();
        Task<ServiceResponse<SeguimientoDTO>> GetSeguimientoById(int id);
        Task<ServiceResponse<SeguimientoDTO>> CreateSeguimiento(SeguimientoDTO seguimientoDTO);
        Task<ServiceResponse<SeguimientoDTO>> UpdateSeguimiento(int id, SeguimientoDTO seguimientoDTO);
        Task<ServiceResponse<SeguimientoDTO>> DeleteSeguimiento(int id);
    }
}
