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
            var areas = await _context.Areas.ToListAsync();
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
            
            var area = new Area(areaDTO.Nome, areaDTO.Gestor);
            await _context.Areas.AddAsync(area);
            await _context.SaveChangesAsync();
            return Ok("Área criada com sucesso!");
        }
    }
}
