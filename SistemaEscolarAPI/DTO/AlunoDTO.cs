using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaEscolarAPI.DTO
{
    public class AlunoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int CursoId { get; set; }
        public string CursoNome { get; set; } = string.Empty;
    }

    public class AlunoCreateDTO
    {
        public string Nome { get; set; } = string.Empty;
        public int CursoId { get; set; }
    }

    public class AlunoDetailsDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public CursoDTO Curso { get; set; } = new CursoDTO();
        public List<DisciplinaDTO> Disciplinas { get; set; } = new();
    }
}