using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgroConnect.Application.Interfaces;
using AgroConnect.Domain.Entities;
using AgroConnect.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace AgroConnect.Infrastructure.Data.Repositories
{
    public class CulturaRepository : ICulturaRepository
    {
        private readonly AgroConnectDbContext _context;

        public CulturaRepository(AgroConnectDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Cultura cultura)
        {
            await _context.Culturas.AddAsync(cultura);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var cultura = await GetByIdAsync(id);
            if (cultura != null)
            {
                _context.Culturas.Remove(cultura);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Cultura>> GetAllAsync()
        {
            return await _context.Culturas
                .Include(c => c.FazendaCulturas)
                .ToListAsync();
        }

        public async Task<Cultura> GetByIdAsync(Guid id)
        {
            return await _context.Culturas
                .Include(c => c.FazendaCulturas)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Cultura>> GetByCategoriaAsync(CategoriaCultura categoria)
        {
            return await _context.Culturas
                .Include(c => c.FazendaCulturas)
                .Where(c => c.Categoria == categoria)
                .ToListAsync();
        }

        public async Task UpdateAsync(Cultura cultura)
        {
            _context.Culturas.Update(cultura);
            await _context.SaveChangesAsync();
        }
    }
}
