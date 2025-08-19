using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RH.API.Contexts;
using RH.API.DTOs;
using RH.API.Models;

namespace RH.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private readonly RHContext _context;
        public FuncionarioController(RHContext context) 
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetFuncionarios()
        {
            var funcionarios = await _context.Funcionarios.Where(f => f.Ativo).ToListAsync();
            return Ok(funcionarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFuncionario(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("ID não pode ser nulo");
            }

            var funcionario = await _context.Funcionarios.FirstOrDefaultAsync(f => f.Id == id && f.Ativo);
            if (funcionario == null)
            {
                return NotFound("Funcionario não encontrado");
            }

            return Ok(funcionario);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFuncionario([FromBody] FuncionarioDTO funcionarioDTO)
        {
            if (funcionarioDTO == null)
            {
                return BadRequest("Dados Inválidos.");
            }
            if (string.IsNullOrEmpty(funcionarioDTO.Nome) || string.IsNullOrEmpty(funcionarioDTO.Documento))
            {
                return BadRequest("Nome e Documento são obrigatórios!");
            }

            var area = await _context.Areas.FirstOrDefaultAsync(f => f.Id == funcionarioDTO.Area && f.Ativo);
            if (area == null)
            {
                return BadRequest("Área não encontrada. O funcionário deve pertencer a uma área válida.");
            }

            var funcionario = new Funcionario(
                funcionarioDTO.Nome,
                funcionarioDTO.DataNascimento,
                funcionarioDTO.Cargo,
                funcionarioDTO.Salario,
                funcionarioDTO.Documento,
                funcionarioDTO.Area
            );

            await _context.Funcionarios.AddAsync(funcionario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFuncionario), new { id = funcionario.Id }, funcionario);
        }


    }
}
