using API_FerroLaminas.DTO;
using API_FerroLaminas.Models;

namespace API_FerroLaminas.Services
{
    public interface IMaterialService
    {
        ServiceResponse<IEnumerable<Material>> GetAllMaterials();
        Task<ServiceResponse<MaterialDTO>> GetMaterialById(int id);
        ServiceResponse<Material> CreateMaterial(Material material);
        ServiceResponse<Material> UpdateMaterial(Material material);
        Task<ServiceResponse<MaterialDTO>> DeleteMaterial(int id);
    }
}
