using AgroConnect.Application.Dtos;

namespace AgroConnect.Application.Interfaces
{
    public interface IAuthService
    {
        Task<UsuarioTokenDto> Login(UsuarioLoginDto usuarioLogin);
        Task<UsuarioTokenDto> Register(UsuarioRegisterDto usuarioRegister);
        Task<UsuarioTokenDto> RefreshToken(string token, string refreshToken);
        Task RevokeToken(string refreshToken);
        Task RequestPasswordReset(string email);
        Task ResetPassword(string token, string newPassword);
        Task ConfirmEmail(string token);
        string CreatePasswordHash(string password);
    }
}