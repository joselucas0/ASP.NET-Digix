using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace MeuProjeto
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            // Habilita arquivos estáticos (permite acessar arquivos da pasta wwwroot)
            app.UseStaticFiles();

            // Endpoint para exibir a página de login
            app.MapGet("/login", async context =>
            {
                context.Response.ContentType = "text/html";
                await context.Response.SendFileAsync("wwwroot/login.html");
            });

            // Endpoint para processar o login (recebe os dados do formulário)
            app.MapPost("/login", async context =>
            {
                var form = await context.Request.ReadFormAsync();
                string email = form["email"];
                string senha = form["senha"];

                // Simulação de validação (substitua por uma autenticação real)
                if(email == "admin@example.com" && senha == "123456")
                {
                    await context.Response.WriteAsync("Login realizado com sucesso!");
                }
                else
                {
                    await context.Response.WriteAsync("Credenciais inválidas!");
                }
            });

            // Endpoint padrão para a raiz da aplicação
            app.MapGet("/", async context =>
            {
                context.Response.Redirect("/login");
            });

            app.Run();
        }
    }
}
