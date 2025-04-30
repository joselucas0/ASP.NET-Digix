using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaEscolarAPI.DB;
using SistemaEscolarAPI.Models;
using SistemaEscolarAPI.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaEscolarAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AlunoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Aluno
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlunoDTO>>> GetAlunos()
        {
            var alunos = await _context.Alunos
                .Include(a => a.Curso)
                .Select(a => new AlunoDTO
                {
                    Id = a.Id,
                    Nome = a.Nome,
                    CursoNome = a.Curso.Descricao
                })
                .ToListAsync();

            return Ok(alunos);
        }

        // GET: api/Aluno/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AlunoDTO>> GetAluno(int id)
        {
            var aluno = await _context.Alunos
                .Include(a => a.Curso)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (aluno == null)
                return NotFound();

            var alunoDTO = new AlunoDTO
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                CursoNome = aluno.Curso.Descricao
            };

            return Ok(alunoDTO);
        }

        // POST: api/Aluno
        [HttpPost]
        public async Task<ActionResult<AlunoDTO>> PostAluno([FromBody] AlunoDTO alunoDTO)
        {
            // Busca pelo NOME do curso (case-insensitive)
            var nomeCurso = alunoDTO.CursoNome.Trim().ToLower();
            var curso = await _context.Cursos
                .FirstOrDefaultAsync(c => c.Nome.Trim().ToLower() == nomeCurso);

            if (curso == null)
                return BadRequest($"Curso '{alunoDTO.CursoNome}' não encontrado");

            var aluno = new Aluno
            {
                Nome = alunoDTO.Nome,
                CursoId = curso.Id
            };

            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();

            // Retorna um DTO simples (evita referências circulares)
            return CreatedAtAction(nameof(GetAluno), new { id = aluno.Id }, new AlunoDTO
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                CursoNome = curso.Nome
            });
        }

        // PUT: api/Aluno/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAluno(int id, [FromBody] AlunoDTO alunoDTO)
        {
            if (id != alunoDTO.Id)
                return BadRequest("ID do aluno não corresponde");

            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno == null)
                return NotFound();

            var curso = await _context.Cursos
                .FirstOrDefaultAsync(c => c.Descricao == alunoDTO.CursoNome);

            if (curso == null)
                return BadRequest("Curso não encontrado");

            aluno.Nome = alunoDTO.Nome;
            aluno.CursoId = curso.Id;

            _context.Entry(aluno).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlunoExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/Aluno/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAluno(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno == null)
                return NotFound();

            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlunoExists(int id)
        {
            return _context.Alunos.Any(e => e.Id == id);
        }
    }
}