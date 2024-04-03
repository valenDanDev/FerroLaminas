using API_FerroLaminas.DTO;
using API_FerroLaminas.Models;
using API_FerroLaminas.Repositories;

namespace API_FerroLaminas.Services
{
    public class TipoCorteService: ITipoCorteService
    {
        private readonly ITipoCorteRepository _tipoCorteRepository;

        public TipoCorteService(ITipoCorteRepository tipoCorteRepository)
        {
            _tipoCorteRepository = tipoCorteRepository;
        }

        public IEnumerable<TipoCorteDTO> GetAllTiposCorte()
        {
            var tiposCorte = _tipoCorteRepository.GetTiposCorte();
            var tiposCorteDTO = new List<TipoCorteDTO>();
            foreach (var tipoCorte in tiposCorte)
            {
                var tipoCorteDTO = new TipoCorteDTO
                {
                    Id = tipoCorte.Id,
                    Nombre = tipoCorte.Nombre,
                    PrecioPorKilo = tipoCorte.PrecioPorKilo,
                    Descripcion = tipoCorte.Descripcion,
                    ServicioId = tipoCorte.ServicioId
                };
                tiposCorteDTO.Add(tipoCorteDTO);
            }
            return tiposCorteDTO;
        }


        public TipoCorteDTO GetTipoCorteById(int id)
        {
            var tipoCorte = _tipoCorteRepository.GetTipoCorteById(id);
            if (tipoCorte == null)
            {
                return null;
            }

            var tipoCorteDTO = new TipoCorteDTO
            {
                Id = tipoCorte.Id,
                Nombre = tipoCorte.Nombre,
                PrecioPorKilo = tipoCorte.PrecioPorKilo,
                Descripcion = tipoCorte.Descripcion,
                ServicioId = tipoCorte.ServicioId
            };
            return tipoCorteDTO;
        }

        public ServiceResponse<TipoCorteDTO> CreateTipoCorte(TipoCorteDTO tipoCorteDTO)
        {
            try
            {
                var tipoCorte = new TipoCorte
                {
                    Nombre = tipoCorteDTO.Nombre,
                    PrecioPorKilo = tipoCorteDTO.PrecioPorKilo,
                    Descripcion = tipoCorteDTO.Descripcion,
                    ServicioId = tipoCorteDTO.ServicioId
                };

                _tipoCorteRepository.CreateTipoCorte(tipoCorte);

                var tipoCorteCreatedDTO = new TipoCorteDTO
                {
                    Id = tipoCorte.Id,
                    Nombre = tipoCorte.Nombre,
                    PrecioPorKilo = tipoCorte.PrecioPorKilo,
                    Descripcion = tipoCorte.Descripcion,
                    ServicioId = tipoCorte.ServicioId
                };

                return new ServiceResponse<TipoCorteDTO>
                {
                    Success = true,
                    Data = tipoCorteCreatedDTO
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<TipoCorteDTO>
                {
                    Success = false,
                    Message = "Error al crear el tipo de corte: " + ex.Message
                };
            }
        }

        public ServiceResponse<TipoCorteDTO> UpdateTipoCorte(int id, TipoCorteDTO tipoCorteDTO)
        {
            try
            {
                var tipoCorte = _tipoCorteRepository.GetTipoCorteById(id);
                if (tipoCorte == null)
                {
                    return new ServiceResponse<TipoCorteDTO>
                    {
                        Success = false,
                        Message = "El tipo de corte no fue encontrado."
                    };
                }

                tipoCorte.Nombre = tipoCorteDTO.Nombre;
                tipoCorte.PrecioPorKilo = tipoCorteDTO.PrecioPorKilo;
                tipoCorte.Descripcion = tipoCorteDTO.Descripcion;

                _tipoCorteRepository.UpdateTipoCorte(tipoCorte);

                var tipoCorteUpdatedDTO = new TipoCorteDTO
                {
                    Id = tipoCorte.Id,
                    Nombre = tipoCorte.Nombre,
                    PrecioPorKilo = tipoCorte.PrecioPorKilo,
                    Descripcion = tipoCorte.Descripcion,
                    ServicioId = tipoCorte.ServicioId
                };

                return new ServiceResponse<TipoCorteDTO>
                {
                    Success = true,
                    Data = tipoCorteUpdatedDTO
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<TipoCorteDTO>
                {
                    Success = false,
                    Message = "Error al actualizar el tipo de corte: " + ex.Message
                };
            }
        }

        public ServiceResponse<string> DeleteTipoCorte(int id)
        {
            try
            {
                var tipoCorte = _tipoCorteRepository.GetTipoCorteById(id);
                if (tipoCorte == null)
                {
                    return new ServiceResponse<string>
                    {
                        Success = false,
                        Message = "El tipo de corte no fue encontrado."
                    };
                }

                _tipoCorteRepository.DeleteTipoCorte(id);

                return new ServiceResponse<string>
                {
                    Success = true,
                    Data = "El tipo de corte fue eliminado exitosamente."
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = "Error al eliminar el tipo de corte: " + ex.Message
                };
            }
        }
    }
}
