using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaEscolarAPI.Models
{
    public class Curso
    {
            public int Id { get; set; }
            public string Nome { get; set; }
            
            // Relações
            public List<Disciplina> Disciplinas { get; set; } = new();
            public List<Aluno> Alunos { get; set; } = new();
            
        // Navigation property for the relationship with DisciplinaAlunoCurso
        public ICollection<DisciplinaAlunoCurso> DisciplinaAlunoCursos { get; set; }
    }
}