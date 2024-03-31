using API_FerroLaminas.DTO;
using API_FerroLaminas.Models;
using API_FerroLaminas.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API_FerroLaminas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialesController : ControllerBase
    {
        private readonly IMaterialService _materialService;

        public MaterialesController(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MaterialDTO>> GetMateriales()
        {
            try
            {
                var response = _materialService.GetAllMaterials();
                if (!response.Success)
                {
                    return BadRequest(response.Message);
                }
                var materialesDTO = response.Data.Select(material => new MaterialDTO
                {
                    Id = material.Id,
                    Tipo = material.Tipo,
                    PrecioPorKilo = material.PrecioPorKilo,
                    StockKilos = material.StockKilos,
                    Descripcion = material.Descripcion
                    // Puedes mapear otras propiedades según sea necesario
                }).ToList();

                return Ok(materialesDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error al obtener los materiales: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<MaterialDTO> GetMaterial(int id)
        {
            try
            {
                var response = _materialService.GetMaterialById(id);
                if (!response.Success)
                {
                    return NotFound(response.Message);
                }
                var material = response.Data;
                var materialDTO = new MaterialDTO
                {
                    Id = material.Id,
                    Tipo = material.Tipo,
                    PrecioPorKilo = material.PrecioPorKilo,
                    StockKilos = material.StockKilos,
                    Descripcion = material.Descripcion
                    // Puedes mapear otras propiedades según sea necesario
                };
                return Ok(materialDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al obtener el material: " + ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<MaterialDTO> CreateMaterial(MaterialDTO materialDTO)
        {
            try
            {
                var material = new Material
                {
                    Tipo = materialDTO.Tipo,
                    PrecioPorKilo = materialDTO.PrecioPorKilo,
                    StockKilos = materialDTO.StockKilos,
                    Descripcion = materialDTO.Descripcion
                };

                var response = _materialService.CreateMaterial(material);

                if (!response.Success)
                {
                    return BadRequest(response.Message);
                }

                var createdMaterialDTO = new MaterialDTO
                {
                    Id = response.Data.Id,
                    Tipo = response.Data.Tipo,
                    PrecioPorKilo = response.Data.PrecioPorKilo,
                    StockKilos = response.Data.StockKilos,
                    Descripcion = response.Data.Descripcion
                    // Puedes mapear otras propiedades según sea necesario
                };

                return CreatedAtAction(nameof(GetMaterial), new { id = createdMaterialDTO.Id }, createdMaterialDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al crear el material: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMaterial(int id, MaterialDTO materialDTO)
        {
            try
            {
                var material = new Material
                {
                    Id = id,
                    Tipo = materialDTO.Tipo,
                    PrecioPorKilo = materialDTO.PrecioPorKilo,
                    StockKilos = materialDTO.StockKilos,
                    Descripcion = materialDTO.Descripcion
                };

                var response = _materialService.UpdateMaterial(material);

                if (!response.Success)
                {
                    return BadRequest(response.Message);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al actualizar el material: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMaterial(int id)
        {
            try
            {
                var response = _materialService.DeleteMaterial(id);

                if (!response.Success)
                {
                    return NotFound(response.Message);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al eliminar el material: " + ex.Message);
            }
        }
    }
}
