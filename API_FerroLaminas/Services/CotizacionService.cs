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
        private readonly IProyectoRepository _proyectoRepository; // Agrega el repositorio de proyecto
        private readonly IMaterialRepository _materialRepository; // Agrega el repositorio de material
        private readonly IServicioRepository _servicioRepository; // Agrega el repositorio de servicio
        public CotizacionService(ICotizacionRepository cotizacionRepository, IProyectoRepository proyectoRepository, IMaterialRepository materialRepository, IServicioRepository servicioRepository)
        {
            _cotizacionRepository = cotizacionRepository;
            _proyectoRepository = proyectoRepository; // Inyecta el repositorio de proyecto
            _materialRepository = materialRepository; // Inyecta el repositorio de material
            _servicioRepository = servicioRepository;
        }

        public async Task<ServiceResponse<IEnumerable<CotizacionDTO>>> GetAllCotizaciones()
        {
            Console.WriteLine("Este es un mensaje en C#");
            var response = new ServiceResponse<IEnumerable<CotizacionDTO>>();
            try
            {
                var cotizaciones = await _cotizacionRepository.GetAllCotizaciones();
                response.Data = cotizaciones.Select(c => new CotizacionDTO( c.ClienteId, c.ProyectoId, c.MaterialId, c.ServicioId, c.PrecioTotal, c.PesoLamina, c.UsuarioId,c.precioMaterial,c.precioServicio,c.CotizacionFinalizada
                ));
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

                // Initialice CotizacionDTO with all required parameters
                var cotizacionDTO = new CotizacionDTO(
                    cotizacion.ClienteId,
                    cotizacion.ProyectoId,
                    cotizacion.MaterialId,
                    cotizacion.ServicioId,
                    cotizacion.PrecioTotal,
                    cotizacion.PesoLamina,
                    cotizacion.UsuarioId,
                    cotizacion.precioMaterial,
                    cotizacion.precioServicio,
                    cotizacion.CotizacionFinalizada
                );

                return new ServiceResponse<CotizacionDTO>
                {
                    Success = true,
                    Data = cotizacionDTO
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

                // Obtener información del proyecto y del material
                Proyecto proyecto = await _proyectoRepository.GetProyectoById(cotizacion.ProyectoId);
                Material material = await _materialRepository.GetMaterialById(cotizacion.MaterialId);
                Servicio servicio = await _servicioRepository.GetServicioById(cotizacion.ServicioId);

                // Calcular el peso del material
                decimal pesoLamina = CalcularPesoMaterial(proyecto, material);

                // Calcular el precio del material
                decimal precioMaterial = CalcularPrecioMaterial(pesoLamina, material);

                // Calcular el precio del servicio
                decimal precioServicio = CalcularPrecioServicio(pesoLamina, servicio);

                // Actualizar el peso de la lámina en la cotización
                cotizacion.PesoLamina = pesoLamina;
                cotizacion.precioMaterial = precioMaterial;
                cotizacion.precioServicio = precioServicio;
                cotizacion.PrecioTotal = precioMaterial + precioServicio;

                var createdCotizacion = await _cotizacionRepository.CreateCotizacion(cotizacion);
                response.Success = true;
                response.Data = new CotizacionDTO(
                    createdCotizacion.ClienteId,
                    createdCotizacion.ProyectoId,
                    createdCotizacion.MaterialId,
                    createdCotizacion.ServicioId,
                    createdCotizacion.PrecioTotal,
                    createdCotizacion.PesoLamina,
                    createdCotizacion.UsuarioId,
                    createdCotizacion.precioMaterial,
                    createdCotizacion.precioServicio,
                    createdCotizacion.CotizacionFinalizada
                );
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al crear la cotización: " + ex.Message;
            }
            return response;
        }

        // Método para calcular el peso del material
        private decimal CalcularPesoMaterial(Proyecto proyecto, Material material)
        {
            // Calcular el área de la lámina
            decimal area = proyecto.Largo * proyecto.Ancho;

            // Convertir el calibre a metros
            decimal grosorMetros = proyecto.Calibre / 1000; // 1 metro = 1000 milímetros

            // Calcular el volumen en metros cúbicos
            decimal volumenMetrosCubicos = area * grosorMetros;


            // Convertir la densidad de g/cm³ a kg/m³
            decimal densidadKgPorM3 = material.Densidad * 1000; // Convertir g/cm³ a kg/m³

            // Calcular el peso en kilogramos
            decimal pesoKilogramos = volumenMetrosCubicos * densidadKgPorM3;

            // Actualizar el stock de kilos del material
            material.StockKilos -= pesoKilogramos;

            // Actualizar el material en la base de datos
            _materialRepository.UpdateMaterial(material);

            return pesoKilogramos;
        }

        private decimal CalcularPrecioMaterial(decimal pesoLamina, Material material)
        {
            // Calcular el precio del material multiplicando el peso de la lámina por el precio por kilo del material
            decimal precioMaterial = pesoLamina * material.PrecioPorKilo;
            return precioMaterial;
        }

        // Método para calcular el precio del servicio
        private decimal CalcularPrecioServicio(decimal pesoLamina, Servicio servicio)
        {
            // Calcular el precio del servicio multiplicando el peso de la lámina por el precio por kilo del servicio
            decimal precioServicio = pesoLamina * servicio.PrecioPorKilo;
            return precioServicio;
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
                response.Data = new CotizacionDTO(
                    updatedCotizacion.ClienteId,
                    updatedCotizacion.ProyectoId,
                    updatedCotizacion.MaterialId,
                    updatedCotizacion.ServicioId,
                    updatedCotizacion.PrecioTotal,
                    updatedCotizacion.PesoLamina,
                    updatedCotizacion.UsuarioId,
                    updatedCotizacion.precioMaterial,
                    updatedCotizacion.precioServicio,
                    updatedCotizacion.CotizacionFinalizada
                );
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
                    response.Data = new CotizacionDTO(
                        deletedCotizacion.ClienteId,
                        deletedCotizacion.ProyectoId,
                        deletedCotizacion.MaterialId,
                        deletedCotizacion.ServicioId,
                        deletedCotizacion.PrecioTotal,
                        deletedCotizacion.PesoLamina,
                        deletedCotizacion.UsuarioId,
                        deletedCotizacion.precioMaterial,
                        deletedCotizacion.precioServicio,
                        deletedCotizacion.CotizacionFinalizada
                    );
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
