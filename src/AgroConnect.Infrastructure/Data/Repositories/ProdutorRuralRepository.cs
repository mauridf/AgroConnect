using AgroConnect.Application.Interfaces;
using AgroConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgroConnect.Infrastructure.Data.Repositories
{
    public class ProdutorRuralRepository : IProdutorRuralRepository
    {
        private readonly AgroConnectDbContext _context;

        public ProdutorRuralRepository(AgroConnectDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ProdutorRural produtor)
        {
            await _context.ProdutoresRurais.AddAsync(produtor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var produtor = await GetByIdAsync(id);
            if (produtor != null)
            {
                _context.ProdutoresRurais.Remove(produtor);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ProdutorRural>> GetAllAsync()
        {
            return await _context.ProdutoresRurais.ToListAsync();
        }

        public async Task<ProdutorRural> GetByIdAsync(Guid id)
        {
            return await _context.ProdutoresRurais.FindAsync(id);
        }

        public async Task<ProdutorRural> GetByCpfAsync(string cpf)
        {
            return await _context.ProdutoresRurais
                .FirstOrDefaultAsync(p => p.CPF == cpf);
        }

        public async Task<IEnumerable<ProdutorRural>> GetByUfAsync(string uf)
        {
            return await _context.ProdutoresRurais
                .Where(p => p.Endereco.UF == uf)
                .ToListAsync();
        }

        public async Task UpdateAsync(ProdutorRural produtor)
        {
            _context.ProdutoresRurais.Update(produtor);
            await _context.SaveChangesAsync();
        }
    }
}
