using API_FerroLaminas.DTO;
using API_FerroLaminas.Models;

namespace API_FerroLaminas.Services
{
    public interface IUbicacionesService
    {
        IEnumerable<UbicacionDTO> GetAllLocations();
    }
}
