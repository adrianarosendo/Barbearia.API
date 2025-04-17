using Barbearia.API.DTO;
using Barbearia.API.Infrastructure;
using Barbearia.API.Models;
using Barbearia.API.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Barbearia.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ServicoController : ControllerBase
    {
        private readonly IRepository<Servico> _repository;

        private readonly IDistributedCache _distributedCache;

        public ServicoController (IRepository<Servico> repository, IDistributedCache cache)
        {
            _repository = repository;
            _distributedCache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var servicos = await _repository.GetAllAsync();
            return Ok(servicos);
        }

        

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var servico = await _repository.GetByIdAsync(id);


            if (servico == null)
            {
                return NotFound();
            }

            return Ok(servico);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ServicoDTO servicoDTO)
        {
            if (ModelState.IsValid)
            {
                Servico servico = new Servico();
                servico.NomeServico = servicoDTO.NomeServico;
                servico.Preco = servicoDTO.Preco;
                servico.DuracaoMin = servicoDTO.DuracaoMin;
                await _repository.AddAsync(servico);

                return CreatedAtAction(nameof(GetById), new { id = servico.Id }, servico);



            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, ServicoDTO servicoDTO)
        {
            if (id != servicoDTO.Id)
            {

                return BadRequest();
            }

            if (ModelState.IsValid)
            {

                var servico = _repository.GetByIdAsync(id);
                if (servico == null)
                {

                    return NotFound();
                }

                servico.Result.NomeServico = servicoDTO.NomeServico;
                servico.Result.Preco = servicoDTO.Preco;
                servico.Result.DuracaoMin = servicoDTO.DuracaoMin;
            

                await _repository.UpdateAsync(servico.Result);

                return Ok(servico.Result);

            }

            return BadRequest();

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {

            if (id == 0) { return NotFound(); }
            var servico = await _repository.GetByIdAsync(id);

            if (servico == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(servico.Id);
            return Ok("Serviço excluído");
        }
    }
}
