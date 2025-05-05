using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaEscolarAPI.DTO
{
    public class LoginDTO
    {
        public string Username {get; set;} = string.Empty; // Aqui para inicializar vazio para evitar o null

        public string Password {get; set;} = string.Empty;// Aqui para inicializar vazio para evitar o null
    }
}