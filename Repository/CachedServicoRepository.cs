
using Barbearia.API.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Barbearia.API.Repository
{
    public class CachedServicoRepository : IRepository<Servico>
    {
        readonly IDistributedCache _cache;
        readonly IRepository<Servico> _servicoRepository;

        public CachedServicoRepository(IRepository<Servico> repository, IDistributedCache distributedCache)
        {
            _servicoRepository = repository;
            _cache = distributedCache;
        }
        public async Task AddAsync(Servico entity)
        {
            // Chama o método do repositório original
            await _servicoRepository.AddAsync(entity);

            await _cache.RemoveAsync("Servicos");
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public  async Task<IEnumerable<Servico>> GetAllAsync()
        {
            var cacheKey = "Servicos";

            var cachedServicos = await _cache.GetAsync(cacheKey);
            IEnumerable<Servico> servicos;

            if (cachedServicos != null)
            {
                var cachedString = System.Text.Encoding.UTF8.GetString(cachedServicos);
                servicos = JsonSerializer.Deserialize<IEnumerable<Servico>>(cachedString);
            }
            else
            {

                servicos = await _servicoRepository.GetAllAsync();
                var serializedServicos = JsonSerializer.Serialize(servicos);

                var cacheBytes = System.Text.Encoding.UTF8.GetBytes(serializedServicos);

                var cacheOptions = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                };

                await _cache.SetAsync(cacheKey, cacheBytes, cacheOptions);
            }

            return servicos;
        }

        public Task<Servico> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Servico entity)
        {
            throw new NotImplementedException();
        }
    }
}
