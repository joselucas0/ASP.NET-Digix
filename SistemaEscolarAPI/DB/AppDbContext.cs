using Microsoft.EntityFrameworkCore;
using SistemaEscolarAPI.Models;

namespace SistemaEscolarAPI.DB
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<DisciplinaAlunoCurso> DisciplinaAlunoCursos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração da chave primária composta
            modelBuilder.Entity<DisciplinaAlunoCurso>()
                .HasKey(d => new { d.AlunoId, d.CursoId, d.DisciplinaId });
            
            // Configuração de relacionamentos
            modelBuilder.Entity<DisciplinaAlunoCurso>()
                .HasOne(d => d.Aluno)
                .WithMany(a => a.DisciplinaAlunoCursos)
                .HasForeignKey(d => d.AlunoId);

            modelBuilder.Entity<DisciplinaAlunoCurso>()
                .HasOne(d => d.Curso)
                .WithMany(c => c.DisciplinaAlunoCursos)
                .HasForeignKey(d => d.CursoId);

            modelBuilder.Entity<DisciplinaAlunoCurso>()
                .HasOne(d => d.Disciplina)
                .WithMany(d => d.DisciplinaAlunoCursos)
                .HasForeignKey(d => d.DisciplinaId);
        }
    }
}