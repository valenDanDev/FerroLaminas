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
            var materiales = _materialService.GetAllMaterials();
            var materialesDTO = materiales.Select(material => new MaterialDTO
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

        [HttpGet("{id}")]
        public ActionResult<MaterialDTO> GetMaterial(int id)
        {
            var material = _materialService.GetMaterialById(id);
            if (material == null)
            {
                return NotFound();
            }
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

        [HttpPost]
        public ActionResult<MaterialDTO> CreateMaterial(MaterialDTO materialDTO)
        {
            // Aquí puedes mapear el DTO a tu modelo de dominio si es necesario
            var material = new Material
            {
                Tipo = materialDTO.Tipo,
                PrecioPorKilo = materialDTO.PrecioPorKilo,
                StockKilos = materialDTO.StockKilos,
                Descripcion = materialDTO.Descripcion
            };

            // Llama al servicio para crear el material
            var createdMaterial = _materialService.CreateMaterial(material);

            // Puedes mapear el material creado a un DTO si es necesario
            var createdMaterialDTO = new MaterialDTO
            {
                Id = createdMaterial.Id,
                Tipo = createdMaterial.Tipo,
                PrecioPorKilo = createdMaterial.PrecioPorKilo,
                StockKilos = createdMaterial.StockKilos,
                Descripcion = createdMaterial.Descripcion
                // Puedes mapear otras propiedades según sea necesario
            };

            return CreatedAtAction(nameof(GetMaterial), new { id = createdMaterialDTO.Id }, createdMaterialDTO);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMaterial(int id, MaterialDTO materialDTO)
        {
            var material = new Material
            {
                Id = id,
                Tipo = materialDTO.Tipo,
                PrecioPorKilo = materialDTO.PrecioPorKilo,
                StockKilos = materialDTO.StockKilos,
                Descripcion = materialDTO.Descripcion
            };

            _materialService.UpdateMaterial(material);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMaterial(int id)
        {
            _materialService.DeleteMaterial(id);
            return NoContent();
        }
    }
}
