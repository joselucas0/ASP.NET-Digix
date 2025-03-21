using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace MeuPortfolio
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            // Habilita arquivos estáticos e define o arquivo padrão como index.html
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.Run();
        }
    }
}
