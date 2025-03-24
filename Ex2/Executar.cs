using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
// Ensure the required dependencies are installed:
// - Microsoft.AspNetCore.Mvc for AddControllers()
// - Swashbuckle.AspNetCore for AddSwaggerGen()

namespace Ex2
{
    public class Executar
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddControllers(); // Requires Microsoft.AspNetCore.Mvc
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(); // Requires Swashbuckle.AspNetCore

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}