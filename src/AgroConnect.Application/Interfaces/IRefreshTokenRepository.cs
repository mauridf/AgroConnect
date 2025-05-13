using AgroConnect.Domain.Entities;

namespace AgroConnect.Application.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetByTokenAsync(string token);
        Task AddAsync(RefreshToken token);
        Task UpdateAsync(RefreshToken token);
        Task RevokeAllForUserAsync(Guid usuarioId);
        Task<bool> TokenExistsAsync(string token);
    }
}
