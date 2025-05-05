using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaEscolarAPI.DTO
{
    public class DisciplinaAlunoCursoDTO
    {
        public int Id { get; set; }
        public int AlunoId { get; set; }
        public string AlunoNome { get; set; } = string.Empty;
        public int CursoId { get; set; }
        public string CursoDescricao { get; set; } = string.Empty;
        public int DisciplinaId { get; set; }
        public string DisciplinaDescricao { get; set; } = string.Empty;
        public decimal Nota { get; set; }
    }
}