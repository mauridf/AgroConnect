using AgroConnect.Application.Interfaces;
using AgroConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgroConnect.Infrastructure.Data.Repositories
{
    public class FazendaRepository : IFazendaRepository
    {
        private readonly AgroConnectDbContext _context;

        public FazendaRepository(AgroConnectDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Fazenda fazenda)
        {
            await _context.Fazendas.AddAsync(fazenda);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var fazenda = await GetByIdAsync(id);
            if (fazenda != null)
            {
                _context.Fazendas.Remove(fazenda);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Fazenda>> GetAllAsync()
        {
            return await _context.Fazendas
                .Include(f => f.Produtor)
                .Include(f => f.FazendaCulturas)
                    .ThenInclude(fc => fc.Cultura)
                .ToListAsync();
        }

        public async Task<Fazenda> GetByIdAsync(Guid id)
        {
            return await _context.Fazendas
                .Include(f => f.Produtor)
                .Include(f => f.FazendaCulturas)
                    .ThenInclude(fc => fc.Cultura)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Fazenda> GetByCnpjAsync(string cnpj)
        {
            return await _context.Fazendas
                .Include(f => f.Produtor)
                .Include(f => f.FazendaCulturas)
                    .ThenInclude(fc => fc.Cultura)
                .FirstOrDefaultAsync(f => f.CNPJ == cnpj);
        }

        public async Task<IEnumerable<Fazenda>> GetByUfAsync(string uf)
        {
            return await _context.Fazendas
                .Include(f => f.Produtor)
                .Include(f => f.FazendaCulturas)
                    .ThenInclude(fc => fc.Cultura)
                .Where(f => f.Endereco.UF == uf)
                .ToListAsync();
        }

        public async Task<IEnumerable<Fazenda>> GetByProdutorIdAsync(Guid produtorId)
        {
            return await _context.Fazendas
                .Include(f => f.Produtor)
                .Include(f => f.FazendaCulturas)
                    .ThenInclude(fc => fc.Cultura)
                .Where(f => f.ProdutorId == produtorId)
                .ToListAsync();
        }

        public async Task UpdateAsync(Fazenda fazenda)
        {
            _context.Fazendas.Update(fazenda);
            await _context.SaveChangesAsync();
        }
    }
}
