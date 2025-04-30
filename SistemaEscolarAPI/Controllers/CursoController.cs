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
    public class CursoController : ControllerBase // Herda de ControllerBase
    {
        private readonly AppDbContext _context;

        public CursoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Curso
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CursoDTO>>> GetCursos()
        {
            var cursos = await _context.Cursos
                .Select(c => new CursoDTO
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Descricao = c.Descricao
                })
                .ToListAsync();

            return Ok(cursos);
        }

        // GET: api/Curso/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CursoDetailsDTO>> GetCurso(int id)
        {
            var curso = await _context.Cursos
                .Include(c => c.Alunos)
                .Include(c => c.Disciplinas)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (curso == null)
                return NotFound();

            var cursoDTO = new CursoDetailsDTO
            {
                Id = curso.Id,
                Nome = curso.Nome,
                Descricao = curso.Descricao,
                Alunos = curso.Alunos.Select(a => new AlunoDTO
                {
                    Id = a.Id,
                    Nome = a.Nome
                }).ToList(),
                Disciplinas = curso.Disciplinas.Select(d => new DisciplinaDTO
                {
                    Id = d.Id,
                    Descricao = d.Descricao
                }).ToList()
            };

            return Ok(cursoDTO);
        }

        // POST: api/Curso
        [HttpPost]
        public async Task<ActionResult<Curso>> PostCurso([FromBody] CursoCreateDTO cursoDTO)
        {
            // Validação de nome duplicado
            if (await _context.Cursos.AnyAsync(c => c.Nome == cursoDTO.Nome))
                return BadRequest("Já existe um curso com este nome");

            var curso = new Curso
            {
                Nome = cursoDTO.Nome,
                Descricao = cursoDTO.Descricao
            };

            _context.Cursos.Add(curso);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCurso), new { id = curso.Id }, curso);
        }

        // PUT: api/Curso/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurso(int id, [FromBody] CursoCreateDTO cursoDTO)
        {
            if (id != cursoDTO.Id)
                return BadRequest("ID do curso não corresponde");

            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null)
                return NotFound();

            curso.Nome = cursoDTO.Nome;
            curso.Descricao = cursoDTO.Descricao;

            _context.Entry(curso).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Curso/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurso(int id)
        {
            var curso = await _context.Cursos
                .Include(c => c.Alunos)
                .Include(c => c.Disciplinas)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (curso == null)
                return NotFound();

            // Verificar se há alunos ou disciplinas vinculados
            if (curso.Alunos.Any() || curso.Disciplinas.Any())
                return BadRequest("Não é possível excluir um curso com alunos ou disciplinas vinculados");

            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}