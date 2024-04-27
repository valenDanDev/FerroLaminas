using API_FerroLaminas.DTO;
using API_FerroLaminas.DTOs;
using API_FerroLaminas.Models;
using API_FerroLaminas.Repositories;

namespace API_FerroLaminas.Services
{
    public class CalibreService : ICalibreService
    {

        private readonly ICalibreRepository _calibreRepository;

        public CalibreService(ICalibreRepository calibreRepository)
        {
            _calibreRepository = calibreRepository ?? throw new System.ArgumentNullException(nameof(calibreRepository));
        }

        public IEnumerable<CalibreDTO>? GetCalibresByMaterialId(int id)
        {
            var calibres = _calibreRepository.GetCalibresByMaterialId(id);

            if (calibres != null)
            {
                foreach (var calibre in calibres)
                {
                    var calibreDTO = new CalibreDTO
                    {
                        Id = calibre.Id,
                        MedidaCalibre = calibre.MedidaCalibre,
                        MaterialId = calibre.MaterialId
                    };
                    yield return calibreDTO;
                }
            }
            else
            {
                // Return a default CalibreDTO object (optional)
                yield return new CalibreDTO
                {
                    Id = 0, // Set default values as needed
                    MedidaCalibre = 0,
                    MaterialId = id
                };
            }
        }
    }
}
