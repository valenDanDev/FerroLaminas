using API_FerroLaminas.DTO;
using API_FerroLaminas.Models;

namespace API_FerroLaminas.Services
{
    public interface ITipoCorteService
    {
        IEnumerable<TipoCorteDTO> GetAllTiposCorte();
        TipoCorteDTO GetTipoCorteById(int id);
        ServiceResponse<TipoCorteDTO> CreateTipoCorte(TipoCorteDTO tipoCorteDTO);
        ServiceResponse<TipoCorteDTO> UpdateTipoCorte(int id, TipoCorteDTO tipoCorteDTO);
        ServiceResponse<string> DeleteTipoCorte(int id);
    }
}
