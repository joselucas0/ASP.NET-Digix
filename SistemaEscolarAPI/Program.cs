using Microsoft.EntityFrameworkCore;
using SistemaEscolarAPI.DB;
using SistemaEscolarAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuração do PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("PostgreSQL");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Configuração do MVC e Controladores
builder.Services.AddControllers();

// Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Pipeline de Requisições
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

// Mapeamento dos Controladores
app.MapControllers();

// Migração Automática
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

app.Run();