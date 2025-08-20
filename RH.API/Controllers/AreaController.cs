using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RH.API.Contexts;
using RH.API.DTOs;
using RH.API.Models;

namespace RH.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly RHContext _context;
        public AreaController(RHContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAreas()
        {
            var areas = await _context.Areas.Where(a => a.Ativo).ToListAsync();
            return Ok(areas);
        }

        [HttpPost]
        public async Task<IActionResult> CreateArea([FromBody] AreaDTO areaDTO)
        {
            if (areaDTO == null)
            {
                return BadRequest("Área não pode ser nula.");
            }
            if (string.IsNullOrEmpty(areaDTO.Nome))
            {
                return BadRequest("O nome da Área deve ser obrigatório.");
            }

            if (areaDTO.Gestor is not null)
            {
                var gestor = await _context.Funcionarios.FindAsync(areaDTO.Gestor);
                if (gestor == null)
                {
                    return NotFound("Funcionário não encontrado");
                }
            } 

            var area = new Area(areaDTO.Nome, areaDTO.Gestor);
            await _context.Areas.AddAsync(area);
            await _context.SaveChangesAsync();
            return Ok("Área criada com sucesso!");
        }

        [HttpPatch("{areaId}")]
        public async Task<IActionResult> EditGestor(Guid areaId, [FromBody] Guid gestorId)
        {
            if (gestorId == Guid.Empty)
            {
                return BadRequest("Gestor não pode ficar em branco.");
            }
            if (areaId == Guid.Empty)
            {
                return BadRequest("Area não pode estar em branco.");
            }

            var area = await _context.Areas.FindAsync(areaId);
            if (area == null)
            {
                return NotFound();
            }

            var gestor = await _context.Funcionarios.FindAsync(gestorId);
            if (gestor == null)
            {
                return NotFound("Funcionário não encontrado");
            }

            area.Gestor = gestor.Id;
            await _context.SaveChangesAsync();
            return Ok("Gestor atualizado.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArea(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Área não pode ficar em branco.");
            }
            
            var area = await _context.Areas.FirstOrDefaultAsync(a => a.Id == id && a.Ativo);
            if (area == null)
            {
                return NotFound();
            }

            var funcionarios = await _context.Funcionarios.Where(f => f.Area == id).ToListAsync();
            if (funcionarios.Any())
            {
                return BadRequest("Não é possível remover uma área que contenha funcionários.");
            }

            area.Desativar();
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
