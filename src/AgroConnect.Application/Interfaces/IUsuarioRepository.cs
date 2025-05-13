using AgroConnect.Domain.Entities;

namespace AgroConnect.Application.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> GetByIdAsync(Guid id);
        Task<Usuario> GetByNomeUsuarioAsync(string nomeUsuario);
        Task<Usuario> GetByEmailAsync(string email);
        Task<Usuario> GetByConfirmationTokenAsync(string token);
        Task<Usuario> GetByPasswordResetTokenAsync(string token);
        Task AddAsync(Usuario usuario);
        Task UpdateAsync(Usuario usuario);
        Task DeleteAsync(Usuario usuario);
        Task<bool> NomeUsuarioExistsAsync(string nomeUsuario);
    }
}