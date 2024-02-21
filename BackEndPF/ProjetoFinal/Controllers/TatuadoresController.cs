using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoFinal.Data;
using ProjetoFinal.Entidades;
using ProjetoFinal.Config;
using ProjetoFinal.Dto;
using Microsoft.IdentityModel.Tokens;

namespace ProjetoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TatuadoresController : ControllerBase
    {
        private readonly ApiContext _context;

        public TatuadoresController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Tatuadores
        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tatuador>>> GetTatudoares()
        {
            return await _context.Tatuadores.ToListAsync();
        }


        [HttpPost]
        public async Task<ActionResult<Tatuador>> PostTatuadores(Tatuador tatuador)
        {
            _context.Tatuadores.Add(tatuador);
            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpPost("login")]
        public async Task<ActionResult<dynamic>> login(TatuadorDto tatuador)
        {
            string token = "";
            var tatuadores = await _context.Tatuadores.ToListAsync();
            var tatuadoresBd = (from u in tatuadores
                                where u.Nome == tatuador.UserName & u.Senha == tatuador.Password
                                select u).ToList();
            if (!tatuadoresBd.IsNullOrEmpty())
            {
                Tatuador tatuadorLogado = tatuadoresBd[0];
                token = TokenService.GenerateTokenT(tatuadorLogado);
            }

            return new { token = token };
        }

        // PUT: api/Tatuadores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTatuador(int id, Tatuador tatuador)
        {
            if (id != tatuador.Id)
            {
                return BadRequest();
            }

            _context.Entry(tatuador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TatuadorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Tatuadores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        // DELETE: api/Tatuadores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTatuador(int id)
        {
            var tatuador = await _context.Tatuadores.FindAsync(id);
            if (tatuador == null)
            {
                return NotFound();
            }

            _context.Tatuadores.Remove(tatuador);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TatuadorExists(int id)
        {
            return _context.Tatuadores.Any(e => e.Id == id);
        }
    }
}
