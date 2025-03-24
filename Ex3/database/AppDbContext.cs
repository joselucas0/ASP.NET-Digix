using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

// Os 3 comandos para rodar Entity Framework Core:
// dotnet add package Microsoft.EntityFrameworkCore
// dotnet add package Microsoft.EntityFrameworkCore.Design
// dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL

namespace Ex3.database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Vamos criar um DbSet para cada tabela do banco de dados
        public DbSet<Models.Usuario> Usuarios { get; set; }
    }
}