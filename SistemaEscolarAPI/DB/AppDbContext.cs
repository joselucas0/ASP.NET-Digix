using Microsoft.EntityFrameworkCore;
using SistemaEscolarAPI.Models;

namespace SistemaEscolarAPI.DB
{
    public class AppDbContext : DbContext
    {
        // Construtor para injeção de dependência
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<DisciplinaAlunoCurso> DisciplinaAlunoCursos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Mantém configurações padrão

            // Configuração da entidade de junção
            modelBuilder.Entity<DisciplinaAlunoCurso>(entity =>
            {
                // Chave primária composta
                entity.HasKey(d => new { d.AlunoId, d.CursoId, d.DisciplinaId });

                // Relacionamentos
                entity.HasOne(d => d.Aluno)
                    .WithMany(a => a.DisciplinaAlunoCursos)
                    .HasForeignKey(d => d.AlunoId)
                    .OnDelete(DeleteBehavior.Restrict); // Evita cascateamento

                entity.HasOne(d => d.Curso)
                    .WithMany(c => c.DisciplinaAlunoCursos)
                    .HasForeignKey(d => d.CursoId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Disciplina)
                    .WithMany(d => d.DisciplinaAlunoCursos)
                    .HasForeignKey(d => d.DisciplinaId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configurações adicionais (exemplo para Usuario)
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasIndex(u => u.Email).IsUnique(); // Email único
            });
        }
    }
}