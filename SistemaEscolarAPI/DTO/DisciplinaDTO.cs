using System.Collections.Generic;

namespace SistemaEscolarAPI.DTO
{
    // DTO básico para listagem de disciplinas
    public class DisciplinaDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public int CursoId { get; set; }
        public string CursoNome { get; set; } = string.Empty; // Adicionado para resolver CS0117
    }

    // DTO para criação de disciplinas (não inclui Id)
    public class DisciplinaCreateDTO
    {
        public string Descricao { get; set; } = string.Empty;
        public int CursoId { get; set; }
    }

    // DTO detalhado com relacionamentos
    public class DisciplinaDetailsDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public CursoDTO Curso { get; set; } = new CursoDTO();
        public List<AlunoDTO> AlunosMatriculados { get; set; } = new();
    }
}