using API_FerroLaminas.DTO;
using API_FerroLaminas.Models;
using API_FerroLaminas.Repositories;
using System;
using System.Collections.Generic;

namespace API_FerroLaminas.Services
{
    public class CotizacionService : ICotizacionService
    {
        private readonly ICotizacionRepository _cotizacionRepository;

        public CotizacionService(ICotizacionRepository cotizacionRepository)
        {
            _cotizacionRepository = cotizacionRepository;
        }

        public async Task<ServiceResponse<IEnumerable<CotizacionDTO>>> GetAllCotizaciones()
        {
            Console.WriteLine("Este es un mensaje en C#");
            var response = new ServiceResponse<IEnumerable<CotizacionDTO>>();
            try
            {
                var cotizaciones = await _cotizacionRepository.GetAllCotizaciones();
                response.Data = cotizaciones.Select(c => new CotizacionDTO(c));
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener todas las cotizaciones: " + ex.Message;
            }
            return response;
        }

        public ServiceResponse<CotizacionDTO> GetCotizacionById(int id)
        {
            try
            {
                var cotizacion = _cotizacionRepository.GetCotizacionById(id);
                if (cotizacion == null)
                {
                    return new ServiceResponse<CotizacionDTO>
                    {
                        Success = false,
                        Message = "Cotización no encontrada."
                    };
                }
                return new ServiceResponse<CotizacionDTO>
                {
                    Success = true,
                    Data = new CotizacionDTO(cotizacion)
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<CotizacionDTO>
                {
                    Success = false,
                    Message = "Error al obtener la cotización: " + ex.Message
                };
            }
        }

        public async Task<ServiceResponse<CotizacionDTO>> CreateCotizacion(Cotizacion cotizacion)
        {
            var response = new ServiceResponse<CotizacionDTO>();
            try
            {
                var createdCotizacion = await _cotizacionRepository.CreateCotizacion(cotizacion);
                response.Success = true;
                response.Data = new CotizacionDTO(createdCotizacion);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al crear la cotización: " + ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<CotizacionDTO>> UpdateCotizacion(int id, Cotizacion cotizacion)
        {
            var response = new ServiceResponse<CotizacionDTO>();
            try
            {
                var updatedCotizacion = await _cotizacionRepository.UpdateCotizacion(id, cotizacion);
                if (updatedCotizacion == null)
                {
                    return new ServiceResponse<CotizacionDTO>
                    {
                        Success = false,
                        Message = "Cotización no encontrada."
                    };
                }
                response.Success = true;
                response.Data = new CotizacionDTO(updatedCotizacion);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al actualizar la cotización: " + ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<CotizacionDTO>> DeleteCotizacion(int id)
        {
            var response = new ServiceResponse<CotizacionDTO>();
            try
            {
                var deletedCotizacion = await _cotizacionRepository.DeleteCotizacion(id);
                if (deletedCotizacion == null)
                {
                    response.Success = false;
                    response.Message = "Cotización no encontrada.";
                }
                else
                {
                    response.Success = true;
                    response.Data = new CotizacionDTO(deletedCotizacion);
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al eliminar la cotización: " + ex.Message;
            }
            return response;
        }

    }
}
