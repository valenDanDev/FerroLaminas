using API_FerroLaminas.Models;

namespace API_FerroLaminas.Services
{
    public interface IMaterialService
    {
        IEnumerable<Material> GetAllMaterials();
        Material GetMaterialById(int id);
        Material CreateMaterial(Material material);
        void UpdateMaterial(Material material);
        void DeleteMaterial(int id);
    }
}
