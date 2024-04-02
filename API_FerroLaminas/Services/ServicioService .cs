using System;
using System.Collections.Generic;
using API_FerroLaminas.DTO;
using API_FerroLaminas.Models;
using API_FerroLaminas.Repositories;

namespace API_FerroLaminas.Services
{
    public class ServicioService: IServicioService
    {
        private readonly IServicioRepository _servicioRepository;

        public ServicioService(IServicioRepository servicioRepository)
        {
            _servicioRepository = servicioRepository;
        }

        public IEnumerable<Servicio> GetAllServicios()
        {
            return _servicioRepository.GetServicios();
        }

        public Servicio GetServicioById(int id)
        {
            return _servicioRepository.GetServicioById(id);
        }

        public ServiceResponse<ServicioDTO> CreateServicio(ServicioDTO servicioDTO)
        {
            try
            {
                var servicio = new Servicio
                {
                    Nombre = servicioDTO.Nombre,
                    PrecioPorKilo = servicioDTO.PrecioPorKilo,
                    Descripcion = servicioDTO.Descripcion
                };

                _servicioRepository.CreateServicio(servicio);

                var servicioCreatedDTO = new ServicioDTO
                {
                    Id = servicio.Id,
                    Nombre = servicio.Nombre,
                    PrecioPorKilo = servicio.PrecioPorKilo,
                    Descripcion = servicio.Descripcion
                };

                return new ServiceResponse<ServicioDTO>
                {
                    Success = true,
                    Data = servicioCreatedDTO
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<ServicioDTO>
                {
                    Success = false,
                    Message = "Error al crear el servicio: " + ex.Message
                };
            }
        }

        public ServiceResponse<ServicioDTO> UpdateServicio(int id, ServicioDTO servicioDTO)
        {
            try
            {
                var servicio = _servicioRepository.GetServicioById(id);
                if (servicio == null)
                {
                    return new ServiceResponse<ServicioDTO>
                    {
                        Success = false,
                        Message = "El servicio no fue encontrado."
                    };
                }

                servicio.Nombre = servicioDTO.Nombre;
                servicio.PrecioPorKilo = servicioDTO.PrecioPorKilo;
                servicio.Descripcion = servicioDTO.Descripcion;

                _servicioRepository.UpdateServicio(servicio);

                var servicioUpdatedDTO = new ServicioDTO
                {
                    Id = servicio.Id,
                    Nombre = servicio.Nombre,
                    PrecioPorKilo = servicio.PrecioPorKilo,
                    Descripcion = servicio.Descripcion
                };

                return new ServiceResponse<ServicioDTO>
                {
                    Success = true,
                    Data = servicioUpdatedDTO
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<ServicioDTO>
                {
                    Success = false,
                    Message = "Error al actualizar el servicio: " + ex.Message
                };
            }
        }

        public ServiceResponse<string> DeleteServicio(int id)
        {
            try
            {
                var servicio = _servicioRepository.GetServicioById(id);
                if (servicio == null)
                {
                    return new ServiceResponse<string>
                    {
                        Success = false,
                        Message = "El servicio no fue encontrado."
                    };
                }

                _servicioRepository.DeleteServicio(id);

                return new ServiceResponse<string>
                {
                    Success = true,
                    Data = "El servicio fue eliminado exitosamente."
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = "Error al eliminar el servicio: " + ex.Message
                };
            }
        }

    }
}
