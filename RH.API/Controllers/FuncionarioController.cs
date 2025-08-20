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

        [HttpPut("{funcionarioId}")]
        public async Task<IActionResult> EditFuncionario(Guid funcionarioId, [FromBody] FuncionarioDTO funcionarioDTO)
        {
            if (funcionarioId == Guid.Empty)
            {
                return BadRequest("O funcionário não pode ser vazio.");
            }
            if (string.IsNullOrEmpty(funcionarioDTO.Nome) || string.IsNullOrEmpty(funcionarioDTO.Documento))
            {
                return BadRequest("Nome e Documento não podem ficar vazios.");
            }

            var funcionario = await _context.Funcionarios.FirstOrDefaultAsync(f => f.Id == funcionarioId && f.Ativo);
            if (funcionario == null)
            {
                return BadRequest("Funcionário não encontrado.");
            }

            var area = await _context.Funcionarios.FirstOrDefaultAsync(a => a.Id == funcionarioDTO.Area && a.Ativo);
            if (area == null)
            {
                return BadRequest("A área especificada não existe ou está inativa.");
            }

            funcionario.Nome = funcionarioDTO.Nome;
            funcionario.DataNascimento = funcionarioDTO.DataNascimento;
            funcionario.Cargo = funcionarioDTO.Cargo;
            funcionario.Salario = funcionarioDTO.Salario;
            funcionario.Documento = funcionarioDTO.Documento;
            funcionario.Area = funcionarioDTO.Area;

            await _context.SaveChangesAsync();
            return Ok("Funcionario foi atualizado com sucesso.");
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateFuncionario(Guid id, [FromBody] Guid areaId)
        {
            if (areaId == Guid.Empty)
            {
                return BadRequest("Área não pode ser vazia.");
            }

            var area = await _context.Areas.FirstOrDefaultAsync(a => a.Id == areaId && a.Ativo);
            if (area == null)
            {
                return NotFound("Área não encontrada.");
            }

            var funcionario = await _context.Funcionarios.FirstOrDefaultAsync(f => f.Id == id && f.Ativo);
            if (funcionario == null)
            {
                return NotFound("Funcionário não encontrado.");
            }

            funcionario.Area = area.Id;
            await _context.SaveChangesAsync();
            return Ok("Funcionário Atualizado");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFuncionario(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Funcionário não pode ficar em branco.");
            }

            var funcionario = await _context.Funcionarios.FirstOrDefaultAsync(f => f.Id == id && f.Ativo);
            if (funcionario == null)
            { 
                return NotFound("Funcionário não encontrado");
            }

            funcionario.Desativar();
            await _context.SaveChangesAsync();
            return Ok("Funcionário foi desativado com sucesso."); 
        }
    }
}
