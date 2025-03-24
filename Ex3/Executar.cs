using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Chamar as bibliotecas do ASP.NET
// Executar o comando para instalar o pacote: dotnet add package Microsoft.AspNetCore
// Comando para instalar o Swagger: dotnet add package Swashbuckle.AspNetCore
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

// Chamar as bibliotecas do Entity Framework
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Ex3.database;

namespace Ex3
{
    public class Executar
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configurar a string de conexão com o banco de dados
            var conncetionString = builder.Configuration.GetConnectionString("PostgresConnection"); // Pega a string de conexão do arquivo appsettings.json

            // Registar o AppDbContext com Postgres
            builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(conncetionString)); // Adiciona o serviço de banco de dados

            builder.Services.AddControllers(); // Adiciona o serviço de controllers

            // Habilitar o Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Vou Chamar o Swagger
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection(); // Redireciona para HTTPS
            
            app.UseAuthorization(); // Habilita a autorização

            app.MapControllers(); // Mapeia os controllers

            app.Run();
        }
    }
}