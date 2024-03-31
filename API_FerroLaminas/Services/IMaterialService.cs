using API_FerroLaminas.Models;

namespace API_FerroLaminas.Services
{
    public interface IMaterialService
    {
        ServiceResponse<IEnumerable<Material>> GetAllMaterials();
        ServiceResponse<Material> GetMaterialById(int id);
        ServiceResponse<Material> CreateMaterial(Material material);
        ServiceResponse<Material> UpdateMaterial(Material material);
        ServiceResponse<bool> DeleteMaterial(int id);
    }
}
