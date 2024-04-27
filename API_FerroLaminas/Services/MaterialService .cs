using API_FerroLaminas.DTO;
using API_FerroLaminas.Models;
using API_FerroLaminas.Repositories;

namespace API_FerroLaminas.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialRepository _materialRepository;

        public MaterialService(IMaterialRepository materialRepository)
        {
            _materialRepository = materialRepository;
        }

        public ServiceResponse<IEnumerable<Material>> GetAllMaterials()
        {
            try
            {
                var materials = _materialRepository.GetMaterials();
                return new ServiceResponse<IEnumerable<Material>>
                {
                    Success = true,
                    Data = materials
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<IEnumerable<Material>>
                {
                    Success = false,
                    Message = "Error al obtener todos los materiales: " + ex.Message
                };
            }
        }

 
        public async Task<ServiceResponse<MaterialDTO>> GetMaterialById(int id)
        {
            var response = new ServiceResponse<MaterialDTO>();
            try
            {
                var proyecto = await _materialRepository.GetMaterialById(id);
                if (proyecto == null)
                {
                    response.Success = false;
                    response.Message = "Proyecto no encontrado.";
                    return response;
                }

                var proyectoDTO = new MaterialDTO
                {
                    Id = proyecto.Id,
                    Descripcion = proyecto.Descripcion,
                    Tipo = proyecto.Tipo,
                    PrecioPorKilo = proyecto.PrecioPorKilo,
                    StockKilos=proyecto.StockKilos
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

        public ServiceResponse<Material> CreateMaterial(Material material)
        {
            if (string.IsNullOrEmpty(material.Tipo) ||
                material.PrecioPorKilo <= 0 ||
                material.StockKilos <= 0 ||
                string.IsNullOrEmpty(material.Descripcion))
            {
                return new ServiceResponse<Material>
                {
                    Success = false,
                    Message = "Los campos Tipo, PrecioPorKilo, StockKilos y Descripcion son requeridos."
                };
            }

            try
            {
                _materialRepository.CreateMaterial(material);
                return new ServiceResponse<Material>
                {
                    Success = true,
                    Data = material
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Material>
                {
                    Success = false,
                    Message = "Error al crear el material: " + ex.Message
                };
            }
        }

        public ServiceResponse<Material> UpdateMaterial(Material material)
        {
            try
            {
                _materialRepository.UpdateMaterial(material);
                return new ServiceResponse<Material>
                {
                    Success = true,
                    Data = material
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Material>
                {
                    Success = false,
                    Message = "Error al actualizar el material: " + ex.Message
                };
            }
        }

        public async Task<ServiceResponse<MaterialDTO>> DeleteMaterial(int id)
        {
            var response = new ServiceResponse<MaterialDTO>();
            try
            {
                var proyecto = await _materialRepository.GetMaterialById(id);
                if (proyecto == null)
                {
                    response.Success = false;
                    response.Message = "Proyecto no encontrado.";
                    return response;
                }

                await _materialRepository.DeleteMaterial(id);
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
