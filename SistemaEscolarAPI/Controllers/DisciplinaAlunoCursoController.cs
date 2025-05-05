using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaEscolarAPI.DB;
using SistemaEscolarAPI.Models;
using SistemaEscolarAPI.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaEscolarAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DisciplinaAlunoCursoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DisciplinaAlunoCursoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DisciplinaAlunoCursoDTO>>> GetAll()
        {
            var registros = await _context.DisciplinaAlunoCursos
                .Include(dac => dac.Aluno)
                .Include(dac => dac.Curso)
                .Include(dac => dac.Disciplina)
                .Select(dac => new DisciplinaAlunoCursoDTO
                {
                    Id = dac.Id,
                    AlunoId = dac.AlunoId,
                    AlunoNome = dac.Aluno.Nome,
                    CursoId = dac.CursoId,
                    CursoDescricao = dac.Curso.Descricao,
                    DisciplinaId = dac.DisciplinaId,
                    DisciplinaDescricao = dac.Disciplina.Descricao,
                    Nota = dac.Nota
                })
                .ToListAsync();

            return Ok(registros);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DisciplinaAlunoCursoDTO>> GetById(int id)
        {
            var dac = await _context.DisciplinaAlunoCursos
                .Include(d => d.Aluno)
                .Include(d => d.Curso)
                .Include(d => d.Disciplina)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (dac == null)
                return NotFound("Relação não encontrada.");

            var dto = new DisciplinaAlunoCursoDTO
            {
                Id = dac.Id,
                AlunoId = dac.AlunoId,
                AlunoNome = dac.Aluno.Nome,
                CursoId = dac.CursoId,
                CursoDescricao = dac.Curso.Descricao,
                DisciplinaId = dac.DisciplinaId,
                DisciplinaDescricao = dac.Disciplina.Descricao,
                Nota = dac.Nota
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<DisciplinaAlunoCursoDTO>> Create([FromBody] DisciplinaAlunoCursoDTO dto)
        {
            var ent = new DisciplinaAlunoCurso
            {
                AlunoId = dto.AlunoId,
                CursoId = dto.CursoId,
                DisciplinaId = dto.DisciplinaId,
                Nota = dto.Nota
            };
            _context.DisciplinaAlunoCursos.Add(ent);
            await _context.SaveChangesAsync();

            dto.Id = ent.Id;
            return CreatedAtAction(nameof(GetById), new { id = ent.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DisciplinaAlunoCursoDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("ID não corresponde.");

            var ent = await _context.DisciplinaAlunoCursos.FindAsync(id);
            if (ent == null)
                return NotFound();

            ent.AlunoId = dto.AlunoId;
            ent.CursoId = dto.CursoId;
            ent.DisciplinaId = dto.DisciplinaId;
            ent.Nota = dto.Nota;

            _context.Entry(ent).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ent = await _context.DisciplinaAlunoCursos.FindAsync(id);
            if (ent == null)
                return NotFound();

            _context.DisciplinaAlunoCursos.Remove(ent);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}