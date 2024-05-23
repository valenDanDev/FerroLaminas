using API_FerroLaminas.DTOs;
using API_FerroLaminas.Models;
using API_FerroLaminas.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_FerroLaminas.Services
{
    public class EstadoOrdenTrabajoService
    {
        private readonly EstadoOrdenTrabajoRepository _repository;

        public EstadoOrdenTrabajoService(EstadoOrdenTrabajoRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<EstadoOrdenTrabajoDTO>> GetAllEstadosAsync()
        {
            var estados = await _repository.GetAllEstadosAsync();
            return estados.Select(e => new EstadoOrdenTrabajoDTO
            {
                Id = e.Id,
                Nombre = e.Nombre
            }).ToList();
        }
    }
}
