using AgroConnect.Application.Dtos;

namespace AgroConnect.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioDto> GetByIdAsync(Guid id);
        Task<UsuarioDto> GetByNomeUsuarioAsync(string nomeUsuario);
        Task<UsuarioDto> CreateAsync(UsuarioRegisterDto usuarioRegister);
        Task<UsuarioDto> UpdateAsync(Guid id, UsuarioUpdateDto usuarioUpdate);
        Task DeleteAsync(Guid id);
        Task<bool> NomeUsuarioExistsAsync(string nomeUsuario);
    }
}