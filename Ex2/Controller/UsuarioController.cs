using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ex2.Models;

// Para a gente poder fazer os protocolos HTTP, precisamos de um controller da biblioteca do ASP.NET, que é o MVC no qual lá tem o ControllerBase
using Microsoft.AspNetCore.Mvc; // precisamos instalar os pacotes com o comando:
// dotnet add package Microsoft.AspNetCore.Mvc

namespace Ex2.Controller
{
    // Orderências
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private static List<Usuario> usuarios = new List<Usuario>()  
        {  
            new Usuario {Id = 1, Nome = "João", Email = "joao@gmail.com"},  
            new Usuario {Id = 2, Nome = "Maria", Email = "maria@gmail.com"},  
            new Usuario {Id = 3, Nome = "José", Email = "jose@gmail.com"}  
        };

        [HttpGet]
        
        public IEnumerable<Usuario> Get()
        {
            return usuarios;
        }

        [HttpPost]

        public Usuario Poist([FromBody] Usuario usuario)
        {
            usuarios.Add(usuario);
            return usuario;
        }

        [HttpPut("{id}")]
        
        public Usuario Put(int id, [FromBody] Usuario usuario)
        {
            var usuarioAtual = usuarios.FirstOrDefault(u => u.Id == id);
            //firstordefault é um método que retorna o primeiro elemento de uma sequência ou um valor padrão se a sequência não contiver elementos
            //where é um método que filtra uma sequência de valores com base em um predicado
            if (usuarioAtual!= null)
            {
                usuarioAtual.Nome = usuario.Nome;
                usuarioAtual.Email = usuario.Email;
            }
            return usuarioAtual;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var usuario = usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario != null)
            {
                usuarios.Remove(usuario);
            }
        }

    }
}