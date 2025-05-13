using AgroConnect.Application.Interfaces;
using AgroConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgroConnect.Infrastructure.Data.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AgroConnectDbContext _context;

        public RefreshTokenRepository(AgroConnectDbContext context)
        {
            _context = context;
        }

        public async Task<RefreshToken> GetByTokenAsync(string token)
        {
            return await _context.RefreshTokens
                .Include(rt => rt.Usuario)
                .FirstOrDefaultAsync(rt => rt.Token == token);
        }

        public async Task AddAsync(RefreshToken token)
        {
            await _context.RefreshTokens.AddAsync(token);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RefreshToken token)
        {
            _context.RefreshTokens.Update(token);
            await _context.SaveChangesAsync();
        }

        public async Task RevokeAllForUserAsync(Guid usuarioId)
        {
            var tokens = await _context.RefreshTokens
                .Where(rt => rt.UsuarioId == usuarioId && rt.Revoked == null)
                .ToListAsync();

            foreach (var token in tokens)
            {
                token.Revoked = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<bool> TokenExistsAsync(string token)
        {
            return await _context.RefreshTokens
                .AnyAsync(rt => rt.Token == token);
        }
    }
}