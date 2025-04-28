using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaEscolarAPI.Models
{
    public class DisciplinaAlunoCurso
    {
            // Chaves estrangeiras composta
    public int AlunoId { get; set; }
    public int CursoId { get; set; }
    public int DisciplinaId { get; set; }
    
    // Navegações
    public Aluno Aluno { get; set; }
    public Curso Curso { get; set; }
    public Disciplina Disciplina { get; set; }
    }
}