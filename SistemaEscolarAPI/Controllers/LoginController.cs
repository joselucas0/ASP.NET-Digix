using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SistemaEscolarAPI.DTO;
using SistemaEscolarAPI.Models;
using SistemaEscolarAPI.Services;
using SistemaEscolarAPI.DB;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace SistemaEscolarAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Username) ||
                string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest("Usuário e senha são obrigatórios");

                
            // Busca pelo email ou nome:
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == dto.Username || u.Nome == dto.Username);

            if (usuario == null)
                return Unauthorized("Credenciais inválidas");

            // Computa hash da senha informada
            using var sha = SHA256.Create();
            var senhaHash = Convert.ToBase64String(
                sha.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)));

            if (senhaHash != usuario.SenhaHash)
                return Unauthorized("Credenciais inválidas");

            var token = TokenService.GenerateToken(usuario);
            return Ok(new { token });
                }
            }
}
