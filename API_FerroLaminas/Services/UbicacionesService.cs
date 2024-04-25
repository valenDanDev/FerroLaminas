using API_FerroLaminas.DTO;
using API_FerroLaminas.Models;
using API_FerroLaminas.Repositories;
using System.Collections.Generic;

namespace API_FerroLaminas.Services
{
    public class UbicacionesService : IUbicacionesService
    {
        private readonly IUbicacionRepository _ubicacionRepository;

        public UbicacionesService(IUbicacionRepository ubicacionRepository)
        {
            _ubicacionRepository = ubicacionRepository ?? throw new System.ArgumentNullException(nameof(ubicacionRepository));
        }

        public IEnumerable<UbicacionDTO> GetAllLocations()
        {
            var ubicaciones = _ubicacionRepository.GetUbicaciones();
            var ubicacionesDTO = new List<UbicacionDTO>();
            foreach (var ubicacion in ubicaciones)
            {
                var ubicacionDTO = new UbicacionDTO
                {
                    Id = ubicacion.Id,
                    Pais = ubicacion.Pais,
                    Departamento = ubicacion.Departamento,
                    Ciudad = ubicacion.Ciudad
                };
                ubicacionesDTO.Add(ubicacionDTO);
            }
            return ubicacionesDTO;
        }
    }
}
