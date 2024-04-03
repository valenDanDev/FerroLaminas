using API_FerroLaminas.Models;

namespace API_FerroLaminas.Repositories
{
    public interface ITipoCorteRepository
    {
        IEnumerable<TipoCorte> GetTiposCorte();
        TipoCorte GetTipoCorteById(int id);
        void CreateTipoCorte(TipoCorte tipoCorte);
        void UpdateTipoCorte(TipoCorte tipoCorte);
        void DeleteTipoCorte(int id);
    }
}
