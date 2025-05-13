using AgroConnect.Application.Interfaces;
using AgroConnect.Domain.Entities;
using AgroConnect.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AgroConnect.Infrastructure.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AgroConnectDbContext _context;

        public UsuarioRepository(AgroConnectDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> GetByIdAsync(Guid id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<Usuario> GetByNomeUsuarioAsync(string nomeUsuario)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.NomeUsuario == nomeUsuario);
        }

        public async Task AddAsync(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Usuario usuario)
        {
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> NomeUsuarioExistsAsync(string nomeUsuario)
        {
            return await _context.Usuarios
                .AnyAsync(u => u.NomeUsuario == nomeUsuario);
        }

        public async Task<Usuario> GetByEmailAsync(string email)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Usuario> GetByConfirmationTokenAsync(string token)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.EmailConfirmationToken == token);
        }

        public async Task<Usuario> GetByPasswordResetTokenAsync(string token)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.PasswordResetToken == token &&
                                      u.PasswordResetTokenExpires > DateTime.UtcNow);
        }
    }
}