using API_FerroLaminas.Data;
using API_FerroLaminas.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_FerroLaminas.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ApplicationDbContext _context;

        public ClienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> GetAllClientes()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente> GetClienteById(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task<Cliente> CreateCliente(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<Cliente> UpdateCliente(int id, Cliente cliente)
        {
            _context.Entry(cliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<Cliente> DeleteCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return null;
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }
    }
}
