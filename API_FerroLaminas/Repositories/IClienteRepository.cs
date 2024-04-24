using API_FerroLaminas.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_FerroLaminas.Repositories
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> GetAllClientes();
        Task<Cliente> GetClienteById(int id);
        Task<Cliente> CreateCliente(Cliente cliente);
        Task<Cliente> UpdateCliente(int id, Cliente cliente);
        Task<Cliente> DeleteCliente(int id);
    }
}
