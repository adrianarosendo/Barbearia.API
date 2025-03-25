using Barbearia.API.DBContext;
using Barbearia.API.DTO;
using Barbearia.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Barbearia.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AgendamentoController : ControllerBase
    {

        public readonly ApplicationDBContext _dbContext;

        public AgendamentoController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult>Get()
        {
            var agendamentos = await _dbContext.Agendamentos.Include(c => c.Cliente).Include(s => s.Servicos).ToListAsync();
            return Ok(agendamentos);

        }

        [HttpPost]
        public async Task<IActionResult>Create([FromBody] AgendamentoDTO agendamentoDTO)
        {
            if (ModelState.IsValid) {
                Agendamento agendamento = new Agendamento();

                agendamento.ClienteId = agendamentoDTO.ClienteId;
                agendamento.Observacoes = agendamentoDTO.Observacoes;
                agendamento.DataHora = agendamentoDTO.DataHora;

                agendamento.Servicos = new List<Servico>();

                agendamento.Cliente = _dbContext.Clientes.Find(agendamentoDTO.ClienteId);

                foreach (var item in agendamentoDTO.Servicos)
                {
                    var servico = _dbContext.Servicos.Find(item.Id);
                    if (servico == null)
                    {
                        return BadRequest();
                    }
                    agendamento.Servicos.Add(servico);
                }

                _dbContext.Agendamentos.Add(agendamento);
                await _dbContext.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { id = agendamento.AgendamentoId }, agendamento);

            
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            
            var agendamento = await _dbContext.Agendamentos.Include(c => c.Cliente).Include(s => s.Servicos).FirstOrDefaultAsync(c => c.AgendamentoId == id);
                       
            if (agendamento == null)
            {
                return NotFound();
            }
                       
            return Ok(agendamento);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, AgendamentoDTO agendamentoDTO)
        {
            if (id != agendamentoDTO.AgendamentoId) { 
            
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var agendamento = await _dbContext.Agendamentos.
                    Include(c => c.Cliente).
                    Include(s => s.Servicos).
                    FirstOrDefaultAsync(c => c.AgendamentoId == id);

                agendamento.ClienteId = agendamentoDTO.ClienteId;
                agendamento.Observacoes = agendamentoDTO.Observacoes;
                agendamento.DataHora = agendamentoDTO.DataHora;

                agendamento.Servicos = new List<Servico>();

                agendamento.Cliente = _dbContext.Clientes.Find(agendamentoDTO.ClienteId);

                foreach (var item in agendamentoDTO.Servicos)
                {
                    var servico = _dbContext.Servicos.Find(item.Id);
                    if (servico == null)
                    {
                        return BadRequest();
                    }
                    agendamento.Servicos.Add(servico);
                }

                _dbContext.Update(agendamento);
                await _dbContext.SaveChangesAsync();

                return Ok(agendamento);
            
            }

            return BadRequest();

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete( int id)
        {

            if(id == 0) { return NotFound(); }
            var agendamento = await _dbContext.
                Agendamentos.
                Include(c => c.Cliente).
                Include(s => s.Servicos).
                FirstOrDefaultAsync(c => c.AgendamentoId == id);

            if (agendamento == null)
            {
                return NotFound();
            }

            _dbContext.Agendamentos.Remove(agendamento);
            await _dbContext.SaveChangesAsync();
            return Ok("Agendamento excluído");
        }
    }

}
