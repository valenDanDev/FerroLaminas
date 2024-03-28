using System.Collections.Generic;
using API_FerroLaminas.Data;
using API_FerroLaminas.Models;
using Microsoft.EntityFrameworkCore;


namespace API_FerroLaminas.Repositories
{
    public class MaterialRepository : IMaterialRepository
    {
        private readonly ApplicationDbContext _context;

        public MaterialRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Material> GetMaterials()
        {
            Console.WriteLine("entro al serviicio de al repo:");
            return _context.Materiales;
        }

        public Material GetMaterialById(int id)
        {
            return _context.Materiales.Find(id);
        }

        public void CreateMaterial(Material material)
        {
            Console.WriteLine("entro al serviicio de al repo para crear:");
            _context.Materiales.Add(material);
            _context.SaveChanges();
        }

        public void UpdateMaterial(Material material)
        {
            _context.Materiales.Update(material);
            _context.SaveChanges();
        }

        public void DeleteMaterial(Material material)
        {
            _context.Materiales.Remove(material);
            _context.SaveChanges();
        }
    }
}
