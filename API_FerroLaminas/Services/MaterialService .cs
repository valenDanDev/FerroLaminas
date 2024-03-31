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

        public ServiceResponse<Material> GetMaterialById(int id)
        {
            try
            {
                var material = _materialRepository.GetMaterialById(id);
                if (material == null)
                {
                    return new ServiceResponse<Material>
                    {
                        Success = false,
                        Message = "Material no encontrado."
                    };
                }
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
                    Message = "Error al obtener el material: " + ex.Message
                };
            }
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

        public ServiceResponse<bool> DeleteMaterial(int id)
        {
            try
            {
                var material = _materialRepository.GetMaterialById(id);
                if (material == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = "Material no encontrado."
                    };
                }
                _materialRepository.DeleteMaterial(material);
                return new ServiceResponse<bool>
                {
                    Success = true,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "Error al eliminar el material: " + ex.Message
                };
            }
        }
    }
}
