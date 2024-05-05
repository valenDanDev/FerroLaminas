using System.Collections.Generic;
using System.Threading.Tasks;
using API_FerroLaminas.Models;

namespace API_FerroLaminas.Repositories
{
    public interface IOrdenDeTrabajoRepository
    {
        Task<IEnumerable<OrdenDeTrabajo>> GetAllOrdenesDeTrabajo();
        Task<OrdenDeTrabajo> GetOrdenDeTrabajoById(int id);
        Task<OrdenDeTrabajo> CreateOrdenDeTrabajo(OrdenDeTrabajo ordenDeTrabajo);
        Task<OrdenDeTrabajo> UpdateOrdenDeTrabajo(int id, OrdenDeTrabajo ordenDeTrabajo);
        Task<OrdenDeTrabajo> DeleteOrdenDeTrabajo(int id);

        Task<IEnumerable<OrdenDeTrabajo>> GetOrdenesDeTrabajoPendientes();
    }
}
