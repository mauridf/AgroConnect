using AgroConnect.Application.Interfaces;
using AgroConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgroConnect.Infrastructure.Data.Repositories
{
    public class FazendaCulturaRepository : IFazendaCulturaRepository
    {
        private readonly AgroConnectDbContext _context;

        public FazendaCulturaRepository(AgroConnectDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(FazendaCultura fazendaCultura)
        {
            await _context.FazendaCulturas.AddAsync(fazendaCultura);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var fazendaCultura = await GetByIdAsync(id);
            if (fazendaCultura != null)
            {
                _context.FazendaCulturas.Remove(fazendaCultura);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<FazendaCultura> GetByIdAsync(Guid id)
        {
            return await _context.FazendaCulturas
                .Include(fc => fc.Fazenda)
                .Include(fc => fc.Cultura)
                .FirstOrDefaultAsync(fc => fc.Id == id);
        }

        public async Task<IEnumerable<FazendaCultura>> GetByFazendaIdAsync(Guid fazendaId)
        {
            return await _context.FazendaCulturas
                .Include(fc => fc.Fazenda)
                .Include(fc => fc.Cultura)
                .Where(fc => fc.FazendaId == fazendaId)
                .ToListAsync();
        }

        public async Task<decimal> GetAreaTotalUtilizadaAsync(Guid fazendaId)
        {
            return await _context.FazendaCulturas
                .Where(fc => fc.FazendaId == fazendaId)
                .SumAsync(fc => fc.AreaUtilizadaHectares);
        }

        public async Task UpdateAsync(FazendaCultura fazendaCultura)
        {
            _context.FazendaCulturas.Update(fazendaCultura);
            await _context.SaveChangesAsync();
        }
    }
}
