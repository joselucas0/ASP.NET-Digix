using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaEscolarAPI.Models;
using SistemaEscolarAPI.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace SistemaEscolarAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisciplinaAlunoCursoController : ControllerBase
    {
        // Lista em memória para simular o banco de dados (substitua isso por sua lógica real)
        private static List<DisciplinaAlunoCurso> _dados = new List<DisciplinaAlunoCurso>();
        private static int _nextId = 1;

        // GET: api/DisciplinaAlunoCurso
        [HttpGet]
        public ActionResult<IEnumerable<DisciplinaAlunoCurso>> GetDisciplinasAlunosCursos()
        {
            return _dados;
        }

        // GET: api/DisciplinaAlunoCurso/5
        [HttpGet("{id}")]
        public ActionResult<DisciplinaAlunoCurso> GetDisciplinaAlunoCurso(int id)
        {
            var entidade = _dados.FirstOrDefault(e => e.Id == id);
            if (entidade == null)
            {
                return NotFound();
            }
            return entidade;
        }

        // POST: api/DisciplinaAlunoCurso
        [HttpPost]
        public ActionResult<DisciplinaAlunoCurso> PostDisciplinaAlunoCurso(DisciplinaAlunoCurso entidade)
        {
            entidade.Id = _nextId++;
            _dados.Add(entidade);
            return CreatedAtAction(nameof(GetDisciplinaAlunoCurso), new { id = entidade.Id }, entidade);
        }

        // PUT: api/DisciplinaAlunoCurso/5
        [HttpPut("{id}")]
        public IActionResult PutDisciplinaAlunoCurso(int id, DisciplinaAlunoCurso entidade)
        {
            var existing = _dados.FirstOrDefault(e => e.Id == id);
            if (existing == null)
            {
                return NotFound();
            }

            existing.DisciplinaId = entidade.DisciplinaId;
            existing.AlunoId = entidade.AlunoId;
            existing.CursoId = entidade.CursoId;
            existing.Nota = entidade.Nota;

            return NoContent();
        }

        // DELETE: api/DisciplinaAlunoCurso/5
        [HttpDelete("{id}")]
        public IActionResult DeleteDisciplinaAlunoCurso(int id)
        {
            var entidade = _dados.FirstOrDefault(e => e.Id == id);
            if (entidade == null)
            {
                return NotFound();
            }

            _dados.Remove(entidade);
            return NoContent();
        }
    }

    // Defina a classe DisciplinaAlunoCurso dentro do mesmo arquivo (se necessário)
    public class DisciplinaAlunoCurso
    {
        public int Id { get; set; }
        public int DisciplinaId { get; set; }
        public int AlunoId { get; set; }
        public int CursoId { get; set; }
        public decimal Nota { get; set; }
    }
}ComDefaultInterfaceAttribute