using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using SistemaEscolarAPI.DB;
using SistemaEscolarAPI.Models;
using SistemaEscolarAPI.DTO;

namespace SistemaEscolarAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/Usuario
        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> Create([FromBody] UsuarioCreateDTO dto)
        {
            if (await _context.Usuarios.AnyAsync(u => u.Email == dto.Email))
                return BadRequest("Email já cadastrado.");

            // Cria hash simples (para produção, use um hasher mais forte)
            var sha = SHA256.Create();
            var senhaHash = Convert.ToBase64String(sha
                .ComputeHash(Encoding.UTF8.GetBytes(dto.Senha)));

            var usuario = new Usuario
            {
                Nome      = dto.Nome,
                Email     = dto.Email,
                SenhaHash = senhaHash,
                Perfil    = dto.Perfil
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = usuario.Id },
                new UsuarioDTO
                {
                    Id    = usuario.Id,
                    Nome  = usuario.Nome,
                    Email = usuario.Email,
                    Perfil = usuario.Perfil
                });
        }

        // GET: api/Usuario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> GetById(int id)
        {
            var u = await _context.Usuarios.FindAsync(id);
            if (u == null) return NotFound();

            return new UsuarioDTO
            {
                Id     = u.Id,
                Nome   = u.Nome,
                Email  = u.Email,
                Perfil = u.Perfil
            };
        }
    }
}
