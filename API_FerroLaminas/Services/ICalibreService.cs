using API_FerroLaminas.DTO;
using API_FerroLaminas.DTOs;
using API_FerroLaminas.Models;

namespace API_FerroLaminas.Services
{
    public interface ICalibreService
    {
        IEnumerable<CalibreDTO> GetCalibresByMaterialId(int id);
    }
}
