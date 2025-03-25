using Barbearia.API.DBContext;
using Barbearia.API.DTO;
using Barbearia.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Barbearia.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ClienteController : ControllerBase
    {

        public readonly ApplicationDBContext _dbContext;

        public ClienteController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult>Get()
        {
            var clientes = await _dbContext.Clientes.ToListAsync();
            return Ok(clientes);

        }

        [HttpPost]
        public async Task<IActionResult>Create([FromBody] ClienteDTO clienteDTO)
        {
            if (ModelState.IsValid) {
                Cliente cliente = new Cliente();
                cliente.Nome = clienteDTO.Nome;
                cliente.Email = clienteDTO.Email;
                cliente.Telefone = clienteDTO.Telefone;
                cliente.DataNascimento = clienteDTO.DataNascimento;
                _dbContext.Clientes.Add(cliente);
                await _dbContext.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { id = cliente.ClienteId }, cliente);

            
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            
            var cliente = await _dbContext.Clientes.FirstOrDefaultAsync(c => c.ClienteId == id);
                       
            if (cliente == null)
            {
                return NotFound();
            }
                       
            return Ok(cliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, ClienteDTO clienteDTO)
        {
            if (id != clienteDTO.ClienteId) { 
            
                return BadRequest();
            }

            if (ModelState.IsValid) { 
            
                var cliente = _dbContext.Clientes.FirstOrDefault(c => c.ClienteId == id);
                if (cliente == null) {
                
                    return NotFound();
                }

                cliente.Nome = clienteDTO.Nome;
                cliente.Email = clienteDTO.Email;
                cliente.Telefone = clienteDTO.Telefone;
                cliente.DataNascimento = clienteDTO.DataNascimento;

                _dbContext.Update(cliente);
                await _dbContext.SaveChangesAsync();

                return Ok(cliente);
            
            }

            return BadRequest();

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete( int id)
        {

            if(id == 0) { return NotFound(); }
            var cliente = await _dbContext.Clientes.FirstOrDefaultAsync(c => c.ClienteId == id);

            if (cliente == null)
            {
                return NotFound();
            }

            _dbContext.Clientes.Remove(cliente);
            await _dbContext.SaveChangesAsync();
            return Ok("Cliente excluído");
        }
    }

}
