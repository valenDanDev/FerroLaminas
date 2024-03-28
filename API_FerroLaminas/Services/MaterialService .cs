using API_FerroLaminas.Models;
using API_FerroLaminas.Repositories;

namespace API_FerroLaminas.Services
{
    public class MaterialService: IMaterialService
    {
        private readonly IMaterialRepository _materialRepository;

        public MaterialService(IMaterialRepository materialRepository)
        {
            _materialRepository = materialRepository;
        }

        public IEnumerable<Material> GetAllMaterials()
        {
            Console.WriteLine("entro al serviicio del get:");
            return _materialRepository.GetMaterials();
        }

        public Material GetMaterialById(int id)
        {
            return _materialRepository.GetMaterialById(id);
        }

        public Material CreateMaterial(Material material)
        {
            if (string.IsNullOrEmpty(material.Tipo) ||
                material.PrecioPorKilo <= 0 ||
                material.StockKilos <= 0 ||
                string.IsNullOrEmpty(material.Descripcion))
            {
                throw new ArgumentException("Los campos Tipo, PrecioPorKilo, StockKilos y Descripcion son requeridos.");
            }

            _materialRepository.CreateMaterial(material);

            // Devuelve el material creado
            return material;
        }

        public void UpdateMaterial(Material material)
        {
            _materialRepository.UpdateMaterial(material);
        }

        public void DeleteMaterial(int id)
        {
            var material = _materialRepository.GetMaterialById(id);
            if (material != null)
            {
                _materialRepository.DeleteMaterial(material);
            }
        }
    }
}
