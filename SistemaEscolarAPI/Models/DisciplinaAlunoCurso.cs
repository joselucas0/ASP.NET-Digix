using System.ComponentModel.DataAnnotations;

namespace SistemaEscolarAPI.Models
{
    public class DisciplinaAlunoCurso
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int DisciplinaId { get; set; }
        public Disciplina Disciplina { get; set; }

        [Required]
        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; }

        [Required]
        public int CursoId { get; set; }
        public Curso Curso { get; set; }

        [Range(0, 10)]
        public decimal Nota { get; set; }
    }
}