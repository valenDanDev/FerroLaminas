using API_FerroLaminas.DTO;
using API_FerroLaminas.Models;
using API_FerroLaminas.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_FerroLaminas.Services
{
    public class SeguimientoService : ISeguimientoService
    {
        private readonly ISeguimientoRepository _seguimientoRepository;

        public SeguimientoService(ISeguimientoRepository seguimientoRepository)
        {
            _seguimientoRepository = seguimientoRepository;
        }

        public async Task<ServiceResponse<IEnumerable<SeguimientoDTO>>> GetAllSeguimientos()
        {
            var response = new ServiceResponse<IEnumerable<SeguimientoDTO>>();
            try
            {
                var seguimientos = await _seguimientoRepository.GetAllSeguimientos();
                var seguimientosDTO = seguimientos.Select(s => new SeguimientoDTO
                {
                    Id = s.Id,
                    OrdenDeTrabajoId = s.OrdenDeTrabajoId,
                    Fecha = s.Fecha,
                    Avance = s.Avance,
                    Observaciones = s.Observaciones
                });
                response.Data = seguimientosDTO;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener todos los seguimientos: " + ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<SeguimientoDTO>> GetSeguimientoById(int id)
        {
            var response = new ServiceResponse<SeguimientoDTO>();
            try
            {
                var seguimiento = await _seguimientoRepository.GetSeguimientoById(id);
                if (seguimiento == null)
                {
                    response.Success = false;
                    response.Message = "Seguimiento no encontrado.";
                    return response;
                }
                var seguimientoDTO = new SeguimientoDTO
                {
                    Id = seguimiento.Id,
                    OrdenDeTrabajoId = seguimiento.OrdenDeTrabajoId,
                    Fecha = seguimiento.Fecha,
                    Avance = seguimiento.Avance,
                    Observaciones = seguimiento.Observaciones
                };
                response.Data = seguimientoDTO;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener el seguimiento: " + ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<SeguimientoDTO>> CreateSeguimiento(SeguimientoDTO seguimientoDTO)
        {
            var response = new ServiceResponse<SeguimientoDTO>();
            try
            {
                var seguimiento = new Seguimiento
                {
                    OrdenDeTrabajoId = seguimientoDTO.OrdenDeTrabajoId,
                    Fecha = seguimientoDTO.Fecha,
                    Avance = seguimientoDTO.Avance,
                    Observaciones = seguimientoDTO.Observaciones
                };

                await _seguimientoRepository.CreateSeguimiento(seguimiento);

                response.Data = seguimientoDTO;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al crear el seguimiento: " + ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<SeguimientoDTO>> UpdateSeguimiento(int id, SeguimientoDTO seguimientoDTO)
        {
            var response = new ServiceResponse<SeguimientoDTO>();
            try
            {
                var seguimiento = new Seguimiento
                {
                    Id = id,
                    OrdenDeTrabajoId = seguimientoDTO.OrdenDeTrabajoId,
                    Fecha = seguimientoDTO.Fecha,
                    Avance = seguimientoDTO.Avance,
                    Observaciones = seguimientoDTO.Observaciones
                };

                await _seguimientoRepository.UpdateSeguimiento(id, seguimiento);

                response.Data = seguimientoDTO;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al actualizar el seguimiento: " + ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<SeguimientoDTO>> DeleteSeguimiento(int id)
        {
            var response = new ServiceResponse<SeguimientoDTO>();
            try
            {
                var seguimiento = await _seguimientoRepository.GetSeguimientoById(id);

                if (seguimiento == null)
                {
                    response.Success = false;
                    response.Message = "Seguimiento no encontrado.";
                    return response;
                }

                await _seguimientoRepository.DeleteSeguimiento(id);

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al eliminar el seguimiento: " + ex.Message;
            }
            return response;
        }
    }
}
