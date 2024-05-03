using API_FerroLaminas.DTO;
using API_FerroLaminas.Models;
using API_FerroLaminas.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_FerroLaminas.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository ?? throw new ArgumentNullException(nameof(clienteRepository));
        }

        public async Task<ServiceResponse<IEnumerable<ClienteDTO>>> GetAllClientes()
        {
            var response = new ServiceResponse<IEnumerable<ClienteDTO>>();
            try
            {
                var clientes = await _clienteRepository.GetAllClientes();
                response.Data = clientes.Select(c => new ClienteDTO
                {
                    Cedula = c.cedula,
                    Nombre = c.Nombre,
                    Telefono = c.Telefono,
                    Direccion = c.Direccion,
                    Email = c.Email,
                    UbicacionId = c.UbicacionId,
                    // Agregar otras propiedades según sea necesario
                });
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener todos los clientes: " + ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<ClienteDTO>> GetClienteById(int id)
        {
            var response = new ServiceResponse<ClienteDTO>();
            try
            {
                var cliente = await _clienteRepository.GetClienteById(id);
                if (cliente == null)
                {
                    response.Success = false;
                    response.Message = "Cliente no encontrado.";
                }
                else
                {
                    response.Success = true;
                    response.Data = new ClienteDTO
                    {
                        Cedula = cliente.cedula,
                        Nombre = cliente.Nombre,
                        Telefono = cliente.Telefono,
                        Direccion = cliente.Direccion,
                        Email = cliente.Email,
                        UbicacionId = cliente.UbicacionId
                        // Agregar otras propiedades según sea necesario
                    };
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener el cliente: " + ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<ClienteDTO>> CreateCliente(Cliente cliente)
        {
            var response = new ServiceResponse<ClienteDTO>();
            try
            {
                // Verificar si la cédula del cliente ya existe
                var existingCliente = await _clienteRepository.GetClienteById(cliente.cedula);
                if (existingCliente != null)
                {
                    // Si el cliente ya existe, devolver la información del cliente existente
                    response.Success = true;
                   // response.Message = "La cédula del cliente ya existe. Información del cliente:";
                    response.Data = new ClienteDTO
                    {
                        Cedula = existingCliente.cedula,
                        Nombre = existingCliente.Nombre,
                        Telefono = existingCliente.Telefono,
                        Direccion = existingCliente.Direccion,
                        Email = existingCliente.Email,
                        UbicacionId = existingCliente.UbicacionId
                        // Agregar otras propiedades según sea necesario
                    };

                    return response;
                }
                else
                {
                    // Si la cédula no existe, crear el nuevo cliente
                    var createdCliente = await _clienteRepository.CreateCliente(cliente);
                    response.Success = true;
                    response.Message = "Cliente creado exitosamente.";
                    response.Data = new ClienteDTO
                    {
                        Cedula = createdCliente.cedula,
                        Nombre = createdCliente.Nombre,
                        Telefono = createdCliente.Telefono,
                        Direccion = createdCliente.Direccion,
                        Email = createdCliente.Email,
                        UbicacionId = createdCliente.UbicacionId
                        // Agregar otras propiedades según sea necesario
                    };
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al crear el cliente: " + ex.Message;
            }
            return response;
        }


        public async Task<ServiceResponse<ClienteDTO>> UpdateCliente(int id, Cliente cliente)
        {
            var response = new ServiceResponse<ClienteDTO>();
            try
            {
                var updatedCliente = await _clienteRepository.UpdateCliente(id, cliente);
                if (updatedCliente == null)
                {
                    response.Success = false;
                    response.Message = "Cliente no encontrado.";
                }
                else
                {
                    response.Success = true;
                    response.Data = new ClienteDTO
                    {
                        Cedula = updatedCliente.cedula,
                        Nombre = updatedCliente.Nombre,
                        Telefono = updatedCliente.Telefono,
                        Direccion = updatedCliente.Direccion,
                        Email = updatedCliente.Email,
                        UbicacionId = updatedCliente.UbicacionId
                        // Agregar otras propiedades según sea necesario
                    };
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al actualizar el cliente: " + ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<ClienteDTO>> DeleteCliente(int id)
        {
            var response = new ServiceResponse<ClienteDTO>();
            try
            {
                var deletedCliente = await _clienteRepository.DeleteCliente(id);
                if (deletedCliente == null)
                {
                    response.Success = false;
                    response.Message = "Cliente no encontrado.";
                }
                else
                {
                    response.Success = true;
                    response.Data = new ClienteDTO
                    {
                        Cedula = deletedCliente.cedula,
                        Nombre = deletedCliente.Nombre,
                        Telefono = deletedCliente.Telefono,
                        Direccion = deletedCliente.Direccion,
                        Email = deletedCliente.Email,
                        UbicacionId = deletedCliente.UbicacionId
                        // Agregar otras propiedades según sea necesario
                    };
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al eliminar el cliente: " + ex.Message;
            }
            return response;
        }
    }
}
