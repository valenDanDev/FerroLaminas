using API_FerroLaminas.Models;

namespace API_FerroLaminas.Repositories
{
    public interface ICalibreRepository
    {
        List<Calibre> GetCalibresByMaterialId(int materialId);
    }
}
