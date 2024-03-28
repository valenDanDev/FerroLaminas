using API_FerroLaminas.Models;

namespace API_FerroLaminas.Repositories
{
    public interface IMaterialRepository
    {
        IEnumerable<Material> GetMaterials();
        Material GetMaterialById(int id);
        void CreateMaterial(Material material);
        void UpdateMaterial(Material material);
        void DeleteMaterial(Material material);
    }
}
