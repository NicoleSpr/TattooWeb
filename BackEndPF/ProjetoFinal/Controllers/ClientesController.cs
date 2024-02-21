using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoFinal.Entidades;
using ProjetoFinal.Dto;
using Microsoft.IdentityModel.Tokens;
using ProjetoFinal.Config;
using ProjetoFinal.Data;

namespace ProjetoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ApiContext _context;

        public ClientesController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> Getusuarios()
        {
            return await _context.Clientes.ToListAsync();
        }


        [HttpPost]
        public async Task<ActionResult<Cliente>> PostUser(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpPost("login")]
        public async Task<ActionResult<dynamic>> login(ClientesDto cliente)
        {
            string token = "";
            var clientes = await _context.Clientes.ToListAsync();
            var clientesBD = (from u in clientes
                                where u.Nome == cliente.UserName & u.Senha == cliente.Password
                                select u).ToList();
            if (!clientesBD.IsNullOrEmpty())
            {
                Cliente clienteLogado = clientesBD[0];
                token = TokenService.GenerateTokenC(clienteLogado);
            }

            return new { token = token };
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }
    }
}
