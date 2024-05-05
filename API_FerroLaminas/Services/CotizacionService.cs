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
        private readonly IUbicacionRepository _ubicacionRepository; // Agrega el repositorio de servicio
        private readonly IOrdenDeTrabajoService _ordenDeTrabajoService;
        public CotizacionService(ICotizacionRepository cotizacionRepository, IProyectoRepository proyectoRepository, IMaterialRepository materialRepository, IServicioRepository servicioRepository, IUbicacionRepository ubicacionRepository, IOrdenDeTrabajoService ordenDeTrabajoService)
        {
            _cotizacionRepository = cotizacionRepository;
            _proyectoRepository = proyectoRepository; // Inyecta el repositorio de proyecto
            _materialRepository = materialRepository; // Inyecta el repositorio de material
            _servicioRepository = servicioRepository;
            _ubicacionRepository = ubicacionRepository;
            _ordenDeTrabajoService = ordenDeTrabajoService;
        }

        public async Task<ServiceResponse<IEnumerable<CotizacionVistaDTO>>> GetAllCotizaciones()
        {
            Console.WriteLine("Este es un mensaje en C#");
            var response = new ServiceResponse<IEnumerable<CotizacionVistaDTO>>();
            try
            {
                var cotizaciones = await _cotizacionRepository.GetAllCotizaciones();
                response.Data = cotizaciones.Select(c => new CotizacionVistaDTO(c.Id,c.ClienteId, c.ProyectoId, c.MaterialId, c.ServicioId, c.PrecioTotal, c.PesoLamina, c.UsuarioId,c.precioMaterial,c.precioServicio,c.CotizacionFinalizada
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

        public ServiceResponse<CotizacionEdicionDTO> GetCotizacionById(int id)
        {
            try
            {
                var cotizacion = _cotizacionRepository.GetCotizacionById(id);
                if (cotizacion == null)
                {
                    return new ServiceResponse<CotizacionEdicionDTO>
                    {
                        Success = false,
                        Message = "Cotización no encontrada."
                    };
                }

                // Initialice CotizacionDTO with all required parameters
                var cotizacionDTO = new CotizacionEdicionDTO(
                    cotizacion.Id,
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

                // Populate related entity information if available
                if (cotizacion.Cliente != null)
                {
                
                    cotizacionDTO.Nombre = cotizacion.Cliente.Nombre;
                    cotizacionDTO.Telefono = cotizacion.Cliente.Telefono;
                    cotizacionDTO.Direccion = cotizacion.Cliente.Direccion;
                    cotizacionDTO.Email = cotizacion.Cliente.Email;
                }

                if (cotizacion.Proyecto != null)
                {
                    cotizacionDTO.DescripcionProyecto = cotizacion.Proyecto.Descripcion;
                    cotizacionDTO.LargoProyecto = cotizacion.Proyecto.Largo;
                    cotizacionDTO.AnchoProyecto = cotizacion.Proyecto.Ancho;
                    cotizacionDTO.Calibre = cotizacion.Proyecto.Calibre;
                }

                if (cotizacion.Material != null)
                {
                    cotizacionDTO.TipoMaterial = cotizacion.Material.Tipo;
                }

                if (cotizacion.Servicio != null)
                {
                    cotizacionDTO.NombreServicio = cotizacion.Servicio.Nombre;
                }
                if (cotizacion.Cliente.Ubicacion != null)
                {
                    cotizacionDTO.Ubicacion = cotizacion.Cliente.Ubicacion.Ciudad;
                }

                return new ServiceResponse<CotizacionEdicionDTO>
                {
                    Success = true,
                    Data = cotizacionDTO
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<CotizacionEdicionDTO>
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
                var proyecto = await _proyectoRepository.GetProyectoById(cotizacion.ProyectoId);
                var material = await _materialRepository.GetMaterialById(cotizacion.MaterialId);
                var servicio = await _servicioRepository.GetServicioById(cotizacion.ServicioId);

                var cotizacionCalculada = await CalcularCotizacion(cotizacion, proyecto, material, servicio);

                var createdCotizacion = await _cotizacionRepository.CreateCotizacion(cotizacionCalculada);

                response.Success = true;
                response.Data = MapToCotizacionDTO(createdCotizacion);

                if (createdCotizacion.CotizacionFinalizada)
                {
                    await CrearOrdenDeTrabajo(createdCotizacion);
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al crear la cotización: " + ex.Message;
            }

            return response;
        }

        private async Task<Cotizacion> CalcularCotizacion(Cotizacion cotizacion, Proyecto proyecto, Material material, Servicio servicio)
        {
            var pesoLamina = CalcularPesoMaterial(proyecto, material);
            var precioMaterial = CalcularPrecioMaterial(pesoLamina, material);
            var precioServicio = CalcularPrecioServicio(pesoLamina, servicio);

            cotizacion.PesoLamina = pesoLamina;
            cotizacion.precioMaterial = precioMaterial;
            cotizacion.precioServicio = precioServicio;
            cotizacion.PrecioTotal = precioMaterial + precioServicio;

            return cotizacion;
        }

        private CotizacionDTO MapToCotizacionDTO(Cotizacion cotizacion)
        {
            return new CotizacionDTO
            {
                ClienteId = cotizacion.ClienteId,
                ProyectoId = cotizacion.ProyectoId,
                MaterialId = cotizacion.MaterialId,
                ServicioId = cotizacion.ServicioId,
                PrecioTotal = cotizacion.PrecioTotal,
                PesoLamina = cotizacion.PesoLamina,
                UsuarioId = cotizacion.UsuarioId,
                PrecioMaterial = cotizacion.precioMaterial,
                PrecioServicio = cotizacion.precioServicio,
                CotizacionFinalizada = cotizacion.CotizacionFinalizada
            };
        }


        private async Task CrearOrdenDeTrabajo(Cotizacion cotizacion)
        {
            var fechaActualUtc = DateTime.UtcNow;
            var fechaActualColombia = fechaActualUtc.AddHours(-5);
            var fechaFinColombia = CalcularFechaFinColombia(fechaActualColombia);

            var ordenDeTrabajoDTO = new OrdenDeTrabajoDTO
            {
                CotizacionId = cotizacion.Id,
                OperarioId = "sin asignar",
                NombreOperario = "sin asignar",
                FechaInicio = fechaActualColombia,
                FechaFin = fechaFinColombia,
                EstadoId = 1
            };

            await _ordenDeTrabajoService.CreateOrdenDeTrabajo(ordenDeTrabajoDTO);
        }

        private DateTime CalcularFechaFinColombia(DateTime fechaActualColombia)
        {
            var fechaFinColombia = fechaActualColombia;

            for (int i = 0; i < 3; i++)
            {
                fechaFinColombia = fechaFinColombia.AddDays(1);

                while (fechaFinColombia.DayOfWeek == DayOfWeek.Saturday || fechaFinColombia.DayOfWeek == DayOfWeek.Sunday)
                {
                    fechaFinColombia = fechaFinColombia.AddDays(1);
                }
            }

            return fechaFinColombia;
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
                var proyecto = await _proyectoRepository.GetProyectoById(cotizacion.ProyectoId);
                var material = await _materialRepository.GetMaterialById(cotizacion.MaterialId);
                var servicio = await _servicioRepository.GetServicioById(cotizacion.ServicioId);

                var cotizacionCalculada = await CalcularCotizacion(cotizacion, proyecto, material, servicio);

                var updatedCotizacion = await _cotizacionRepository.UpdateCotizacion(id, cotizacionCalculada);
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

                if (updatedCotizacion.CotizacionFinalizada)
                {
                    await CrearOrdenDeTrabajo(updatedCotizacion);
                }
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
