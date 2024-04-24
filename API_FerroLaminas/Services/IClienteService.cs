using API_FerroLaminas.DTO;
using API_FerroLaminas.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_FerroLaminas.Services
{
    public interface IClienteService
    {
        Task<ServiceResponse<IEnumerable<ClienteDTO>>> GetAllClientes();
        Task<ServiceResponse<ClienteDTO>> GetClienteById(int id);
        Task<ServiceResponse<ClienteDTO>> CreateCliente(Cliente cliente);
        Task<ServiceResponse<ClienteDTO>> UpdateCliente(int id, Cliente cliente);
        Task<ServiceResponse<ClienteDTO>> DeleteCliente(int id);
    }
}
