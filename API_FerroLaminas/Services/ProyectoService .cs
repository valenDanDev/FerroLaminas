using API_FerroLaminas.DTO;
using API_FerroLaminas.Models;
using API_FerroLaminas.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_FerroLaminas.Services
{
    public class ProyectoService : IProyectoService
    {
        private readonly IProyectoRepository _proyectoRepository;

        public ProyectoService(IProyectoRepository proyectoRepository)
        {
            _proyectoRepository = proyectoRepository;
        }

        public async Task<ServiceResponse<IEnumerable<ProyectoDTO>>> GetAllProyectos()
        {
            var response = new ServiceResponse<IEnumerable<ProyectoDTO>>();
            try
            {
                var proyectos = await _proyectoRepository.GetAllProyectos();
                var proyectosDTO = new List<ProyectoDTO>();
                foreach (var proyecto in proyectos)
                {
                    var proyectoDTO = new ProyectoDTO
                    {
                        Id = proyecto.Id,
                        Descripcion = proyecto.Descripcion,
                        Largo = proyecto.Largo,
                        Ancho = proyecto.Ancho
                    };
                    proyectosDTO.Add(proyectoDTO);
                }
                response.Data = proyectosDTO;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener todos los proyectos: " + ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<ProyectoDTO>> GetProyectoById(int id)
        {
            var response = new ServiceResponse<ProyectoDTO>();
            try
            {
                var proyecto = await _proyectoRepository.GetProyectoById(id);
                if (proyecto == null)
                {
                    response.Success = false;
                    response.Message = "Proyecto no encontrado.";
                    return response;
                }

                var proyectoDTO = new ProyectoDTO
                {
                    Id = proyecto.Id,
                    Descripcion = proyecto.Descripcion,
                    Largo = proyecto.Largo,
                    Ancho = proyecto.Ancho
                };

                response.Data = proyectoDTO;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener el proyecto: " + ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<ProyectoDTO>> CreateProyecto(ProyectoDTO proyectoDTO)
        {
            var response = new ServiceResponse<ProyectoDTO>();
            try
            {
                var proyecto = new Proyecto
                {
                    Descripcion = proyectoDTO.Descripcion,
                    Largo = proyectoDTO.Largo,
                    Ancho = proyectoDTO.Ancho
                };

                var createdProyecto = await _proyectoRepository.CreateProyecto(proyecto);
                response.Data = proyectoDTO;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al crear el proyecto: " + ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<ProyectoDTO>> UpdateProyecto(int id, ProyectoDTO proyectoDTO)
        {
            var response = new ServiceResponse<ProyectoDTO>();
            try
            {
                var proyecto = new Proyecto
                {
                    Id = id,
                    Descripcion = proyectoDTO.Descripcion,
                    Largo = proyectoDTO.Largo,
                    Ancho = proyectoDTO.Ancho
                };

                var updatedProyecto = await _proyectoRepository.UpdateProyecto(id, proyecto);
                if (updatedProyecto == null)
                {
                    response.Success = false;
                    response.Message = "Proyecto no encontrado.";
                    return response;
                }

                response.Data = proyectoDTO;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al actualizar el proyecto: " + ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<ProyectoDTO>> DeleteProyecto(int id)
        {
            var response = new ServiceResponse<ProyectoDTO>();
            try
            {
                var proyecto = await _proyectoRepository.GetProyectoById(id);
                if (proyecto == null)
                {
                    response.Success = false;
                    response.Message = "Proyecto no encontrado.";
                    return response;
                }

                await _proyectoRepository.DeleteProyecto(id);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al eliminar el proyecto: " + ex.Message;
            }
            return response;
        }
    }
}
