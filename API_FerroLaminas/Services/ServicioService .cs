using System;
using System.Collections.Generic;
using API_FerroLaminas.DTO;
using API_FerroLaminas.Models;
using API_FerroLaminas.Repositories;

namespace API_FerroLaminas.Services
{
    public class ServicioService : IServicioService
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


        public async Task<ServiceResponse<ServicioDTO>> GetServicioById(int id)
        {
            var response = new ServiceResponse<ServicioDTO>();
            try
            {
                var proyecto = await _servicioRepository.GetServicioById(id);
                if (proyecto == null)
                {
                    response.Success = false;
                    response.Message = "Servicio no encontrado.";
                    return response;
                }

                var proyectoDTO = new ServicioDTO
                {
                    Id = proyecto.Id,
                    Descripcion = proyecto.Descripcion,
                    PrecioPorKilo = proyecto.PrecioPorKilo,
                    Nombre = proyecto.Nombre
                };

                response.Data = proyectoDTO;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener el servicio: " + ex.Message;
            }
            return response;
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

        public async Task<ServiceResponse<ServicioDTO>> UpdateServicio(int id, ServicioDTO servicioDTO)
        {
            var response = new ServiceResponse<ServicioDTO>();
            try
            {
                var proyecto = new Servicio
                {
                    Id = id,
                    Descripcion = servicioDTO.Descripcion,
                    Nombre = servicioDTO.Nombre,
                    PrecioPorKilo = servicioDTO.PrecioPorKilo
                };

                var updatedProyecto = await _servicioRepository.UpdateServicio(id, proyecto);
                if (updatedProyecto == null)
                {
                    response.Success = false;
                    response.Message = "Proyecto no encontrado.";
                    return response;
                }

                response.Data = servicioDTO;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al actualizar el proyecto: " + ex.Message;
            }
            return response;
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
