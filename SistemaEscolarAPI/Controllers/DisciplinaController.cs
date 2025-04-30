using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaEscolarAPI.DB;
using SistemaEscolarAPI.DTO;
using SistemaEscolarAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaEscolarAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DisciplinaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DisciplinaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Disciplina
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DisciplinaDTO>>> GetDisciplinas()
        {
            var disciplinas = await _context.Disciplinas
                .Include(d => d.Curso)
                .Select(d => new DisciplinaDTO
                {
                    Id = d.Id,
                    Descricao = d.Descricao,
                    CursoId = d.CursoId,
                    CursoNome = d.Curso != null ? d.Curso.Nome : string.Empty // Tratamento de nulo
                })
                .ToListAsync();

            return Ok(disciplinas);
        }

        // GET: api/Disciplina/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DisciplinaDetailsDTO>> GetDisciplina(int id)
        {
            var disciplina = await _context.Disciplinas
                .Include(d => d.Curso)
                .Include(d => d.DisciplinaAlunoCursos)
                    .ThenInclude(dac => dac.Aluno)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (disciplina == null)
                return NotFound();

            var disciplinaDTO = new DisciplinaDetailsDTO
            {
                Id = disciplina.Id,
                Descricao = disciplina.Descricao,
                Curso = new CursoDTO
                {
                    Id = disciplina.Curso.Id,
                    Nome = disciplina.Curso.Nome,
                    Descricao = disciplina.Curso.Descricao
                },
                AlunosMatriculados = disciplina.DisciplinaAlunoCursos
                    .Select(dac => new AlunoDTO
                    {
                        Id = dac.Aluno.Id,
                        Nome = dac.Aluno.Nome
                    }).ToList()
            };

            return Ok(disciplinaDTO);
        }

        // POST: api/Disciplina
        [HttpPost]
        public async Task<ActionResult<DisciplinaDTO>> PostDisciplina([FromBody] DisciplinaCreateDTO disciplinaDTO)
        {
            // Validação do curso
            var curso = await _context.Cursos.FindAsync(disciplinaDTO.CursoId);
            if (curso == null)
                return BadRequest("Curso não encontrado");

            var disciplina = new Disciplina
            {
                Descricao = disciplinaDTO.Descricao,
                CursoId = disciplinaDTO.CursoId
            };

            _context.Disciplinas.Add(disciplina);
            await _context.SaveChangesAsync();

            // Retorna o DTO básico
            return CreatedAtAction(nameof(GetDisciplina), new { id = disciplina.Id }, new DisciplinaDTO
            {
                Id = disciplina.Id,
                Descricao = disciplina.Descricao,
                CursoId = disciplina.CursoId,
                CursoNome = curso.Nome
            });
        }

        // PUT: api/Disciplina/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDisciplina(int id, [FromBody] DisciplinaCreateDTO disciplinaDTO)
        {
            var disciplina = await _context.Disciplinas.FindAsync(id);
            if (disciplina == null)
                return NotFound();

            // Validação do curso
            var curso = await _context.Cursos.FindAsync(disciplinaDTO.CursoId);
            if (curso == null)
                return BadRequest("Curso não encontrado");

            disciplina.Descricao = disciplinaDTO.Descricao;
            disciplina.CursoId = disciplinaDTO.CursoId;

            _context.Entry(disciplina).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Disciplina/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDisciplina(int id)
        {
            var disciplina = await _context.Disciplinas
                .Include(d => d.DisciplinaAlunoCursos)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (disciplina == null)
                return NotFound();

            if (disciplina.DisciplinaAlunoCursos.Any())
                return BadRequest("Não é possível excluir uma disciplina com alunos matriculados");

            _context.Disciplinas.Remove(disciplina);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}