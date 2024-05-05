using API_FerroLaminas.DTO;
using API_FerroLaminas.DTOs;
using API_FerroLaminas.Models;
using API_FerroLaminas.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_FerroLaminas.Services
{
    public class OrdenDeTrabajoService : IOrdenDeTrabajoService
    {
        private readonly IOrdenDeTrabajoRepository _ordenDeTrabajoRepository;

        public OrdenDeTrabajoService(IOrdenDeTrabajoRepository ordenDeTrabajoRepository)
        {
            _ordenDeTrabajoRepository = ordenDeTrabajoRepository;
        }

        public async Task<ServiceResponse<IEnumerable<OrdenDeTrabajoDTO>>> GetAllOrdenesDeTrabajo()
        {
            var response = new ServiceResponse<IEnumerable<OrdenDeTrabajoDTO>>();
            try
            {
                var ordenesDeTrabajo = await _ordenDeTrabajoRepository.GetAllOrdenesDeTrabajo();
                var ordenesDeTrabajoDTO = ordenesDeTrabajo.Select(o => new OrdenDeTrabajoDTO
                {
                    Id = o.Id,
                    CotizacionId = o.CotizacionId,
                    OperarioId = o.OperarioId,
                    NombreOperario = o.nombreOperario,
                    FechaInicio = o.FechaInicio,
                    FechaFin = o.FechaFin,
                    EstadoId = o.EstadoId,
                });
                response.Data = ordenesDeTrabajoDTO;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener todas las órdenes de trabajo: " + ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<OrdenDeTrabajoDTO>> GetOrdenDeTrabajoById(int id)
        {
            var response = new ServiceResponse<OrdenDeTrabajoDTO>();
            try
            {
                var ordenDeTrabajo = await _ordenDeTrabajoRepository.GetOrdenDeTrabajoById(id);
                if (ordenDeTrabajo == null)
                {
                    response.Success = false;
                    response.Message = "Orden de trabajo no encontrada.";
                    return response;
                }
                var ordenDeTrabajoDTO = new OrdenDeTrabajoDTO
                {
                    Id = ordenDeTrabajo.Id,
                    CotizacionId = ordenDeTrabajo.CotizacionId,
                    OperarioId = ordenDeTrabajo.OperarioId,
                    NombreOperario = ordenDeTrabajo.nombreOperario,
                    FechaInicio = ordenDeTrabajo.FechaInicio,
                    FechaFin = ordenDeTrabajo.FechaFin,
                    EstadoId = ordenDeTrabajo.EstadoId,
                };
                response.Data = ordenDeTrabajoDTO;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener la orden de trabajo: " + ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<OrdenDeTrabajoDTO>> CreateOrdenDeTrabajo(OrdenDeTrabajoDTO ordenDeTrabajoDTO)
        {
            var response = new ServiceResponse<OrdenDeTrabajoDTO>();
            try
            {
                var ordenDeTrabajo = new OrdenDeTrabajo
                {
                    CotizacionId = ordenDeTrabajoDTO.CotizacionId,
                    OperarioId = ordenDeTrabajoDTO.OperarioId,
                    nombreOperario = ordenDeTrabajoDTO.NombreOperario,
                    FechaInicio = ordenDeTrabajoDTO.FechaInicio,
                    FechaFin = ordenDeTrabajoDTO.FechaFin,
                    EstadoId = ordenDeTrabajoDTO.EstadoId
                };

                await _ordenDeTrabajoRepository.CreateOrdenDeTrabajo(ordenDeTrabajo);


                response.Data = ordenDeTrabajoDTO;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al crear la orden de trabajo: " + ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<OrdenDeTrabajoDTO>> UpdateOrdenDeTrabajo(int id, OrdenDeTrabajoDTO ordenDeTrabajoDTO)
        {
            var response = new ServiceResponse<OrdenDeTrabajoDTO>();
            try
            {
                var ordenDeTrabajo = new OrdenDeTrabajo
                {
                    Id = id,
                    CotizacionId = ordenDeTrabajoDTO.CotizacionId,
                    OperarioId = ordenDeTrabajoDTO.OperarioId,
                    nombreOperario = ordenDeTrabajoDTO.NombreOperario,
                    FechaInicio = ordenDeTrabajoDTO.FechaInicio,
                    FechaFin = ordenDeTrabajoDTO.FechaFin,
                    EstadoId = ordenDeTrabajoDTO.EstadoId
                };

                await _ordenDeTrabajoRepository.UpdateOrdenDeTrabajo(id,ordenDeTrabajo);

                response.Data = ordenDeTrabajoDTO;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al actualizar la orden de trabajo: " + ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<OrdenDeTrabajoDTO>> DeleteOrdenDeTrabajo(int id)
        {
            var response = new ServiceResponse<OrdenDeTrabajoDTO>();
            try
            {
                var ordenDeTrabajo = await _ordenDeTrabajoRepository.GetOrdenDeTrabajoById(id);

                await _ordenDeTrabajoRepository.DeleteOrdenDeTrabajo(id);

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al eliminar la orden de trabajo: " + ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<IEnumerable<OrdenDeTrabajoDTO>>> OrdenesTrabajoPendientes()
        {
            var response = new ServiceResponse<IEnumerable<OrdenDeTrabajoDTO>>();
            try
            {
                var ordenesDeTrabajo = await _ordenDeTrabajoRepository.GetOrdenesDeTrabajoPendientes();
                var ordenesDeTrabajoDTO = ordenesDeTrabajo.Select(o => new OrdenDeTrabajoDTO
                {
                    Id = o.Id,
                    CotizacionId = o.CotizacionId,
                    OperarioId = o.OperarioId,
                    NombreOperario = o.nombreOperario,
                    FechaInicio = o.FechaInicio,
                    FechaFin = o.FechaFin,
                    EstadoId = o.EstadoId,
                });
                response.Data = ordenesDeTrabajoDTO;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener las órdenes de trabajo pendientes: " + ex.Message;
            }
            return response;
        }
    
}


}
