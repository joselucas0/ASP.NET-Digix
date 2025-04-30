using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaEscolarAPI.DTO
{
    public class CursoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
    }

    // Para criação/atualização
    public class CursoCreateDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
    }

    // Para detalhamento com relacionamentos
    public class CursoDetailsDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public List<AlunoDTO> Alunos { get; set; } = new();
        public List<DisciplinaDTO> Disciplinas { get; set; } = new();
    }
}