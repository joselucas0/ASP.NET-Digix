using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaEscolarAPI.Models
{
    public class Aluno
    {
            public int Id { get; set; }
        public string Nome { get; set; }
        
        // Chave estrangeira para Curso
        public int CursoId { get; set; }
        public Curso Curso { get; set; }
        
        // Relação muitos-para-muitos
        public List<DisciplinaAlunoCurso> DisciplinaAlunoCursos { get; set; } = new();
    }
}