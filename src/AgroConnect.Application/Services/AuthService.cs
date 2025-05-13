using AgroConnect.Application.Dtos;
using AgroConnect.Application.Interfaces;
using AgroConnect.Domain.Entities;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static AgroConnect.Domain.Entities.EntityBase;

namespace AgroConnect.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly JwtSettings _jwtSettings;

        public AuthService(
            IUsuarioRepository usuarioRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IEmailService emailService,
            IMapper mapper,
            IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _emailService = emailService;
            _mapper = mapper;
            _configuration = configuration;
            _jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>();
        }

        public async Task<UsuarioTokenDto> Login(UsuarioLoginDto usuarioLogin)
        {
            var usuario = await _usuarioRepository.GetByNomeUsuarioAsync(usuarioLogin.NomeUsuario);

            if (usuario == null || !VerifyPasswordHash(usuarioLogin.Senha, usuario.SenhaHash))
                throw new DomainException("Usuário ou senha incorretos");

            var refreshToken = GenerateRefreshToken();
            await _refreshTokenRepository.AddAsync(new RefreshToken
            {
                UsuarioId = usuario.Id,
                Token = refreshToken,
                Expires = DateTime.UtcNow.AddDays(7)
            });

            return new UsuarioTokenDto
            {
                Usuario = _mapper.Map<UsuarioDto>(usuario),
                Token = GenerateJwtToken(usuario),
                RefreshToken = refreshToken,
                DataExpiracao = DateTime.UtcNow.AddHours(_jwtSettings.ExpireHours)
            };
        }

        public async Task<UsuarioTokenDto> Register(UsuarioRegisterDto usuarioRegister)
        {
            if (await _usuarioRepository.NomeUsuarioExistsAsync(usuarioRegister.NomeUsuario))
                throw new DomainException("Nome de usuário já está em uso");

            var usuario = new Usuario(
                usuarioRegister.NomeUsuario,
                CreatePasswordHash(usuarioRegister.Senha),
                usuarioRegister.TipoUsuario,
                email: usuarioRegister.Email, // Adicione esta linha se o email for opcional
                emailConfirmado: false);

            await _usuarioRepository.AddAsync(usuario);

            // Se tiver email, enviar confirmação
            if (!string.IsNullOrEmpty(usuarioRegister.Email))
            {
                usuario.EmailConfirmationToken = GenerateToken();
                await _usuarioRepository.UpdateAsync(usuario);

                var confirmationLink = $"{_configuration["ClientApp:Url"]}/confirmar-email?token={usuario.EmailConfirmationToken}";
                await _emailService.SendConfirmationEmailAsync(usuarioRegister.Email, confirmationLink);
            }

            return new UsuarioTokenDto
            {
                Usuario = _mapper.Map<UsuarioDto>(usuario),
                Token = GenerateJwtToken(usuario),
                DataExpiracao = DateTime.UtcNow.AddHours(_jwtSettings.ExpireHours)
            };
        }

        private string GenerateJwtToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Name, usuario.NomeUsuario),
                    new Claim(ClaimTypes.Role, usuario.TipoUsuario.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpireHours),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string CreatePasswordHash(string password)
        {
            using var hmac = new HMACSHA512();
            var passwordSalt = hmac.Key;
            var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            // Combine salt and hash for storage
            var combinedHash = new byte[passwordSalt.Length + passwordHash.Length];
            Buffer.BlockCopy(passwordSalt, 0, combinedHash, 0, passwordSalt.Length);
            Buffer.BlockCopy(passwordHash, 0, combinedHash, passwordSalt.Length, passwordHash.Length);

            return Convert.ToBase64String(combinedHash);
        }

        private bool VerifyPasswordHash(string password, string storedHash)
        {
            var combinedHash = Convert.FromBase64String(storedHash);
            var salt = new byte[64];
            var hash = new byte[64];

            Buffer.BlockCopy(combinedHash, 0, salt, 0, salt.Length);
            Buffer.BlockCopy(combinedHash, salt.Length, hash, 0, hash.Length);

            using var hmac = new HMACSHA512(salt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return computedHash.SequenceEqual(hash);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private string GenerateToken()
        {
            return Guid.NewGuid().ToString("N");
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Token inválido");

            return principal;
        }

        public async Task<UsuarioTokenDto> RefreshToken(string token, string refreshToken)
        {
            var principal = GetPrincipalFromExpiredToken(token);
            var claim = principal.FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null || !Guid.TryParse(claim.Value, out var usuarioId))
                throw new DomainException("Token inválido");

            var usuario = await _usuarioRepository.GetByIdAsync(usuarioId);
            if (usuario == null)
                throw new DomainException("Usuário não encontrado");

            var storedRefreshToken = await _refreshTokenRepository.GetByTokenAsync(refreshToken);
            if (storedRefreshToken == null || storedRefreshToken.UsuarioId != usuario.Id || storedRefreshToken.IsExpired)
                throw new DomainException("Refresh token inválido");

            // Revoga o refresh token atual
            storedRefreshToken.Revoked = DateTime.UtcNow;
            await _refreshTokenRepository.UpdateAsync(storedRefreshToken);

            // Gera novos tokens
            var newToken = GenerateJwtToken(usuario);
            var newRefreshToken = GenerateRefreshToken();

            // Armazena o novo refresh token
            await _refreshTokenRepository.AddAsync(new RefreshToken
            {
                UsuarioId = usuario.Id,
                Token = newRefreshToken,
                Expires = DateTime.UtcNow.AddDays(7)
            });

            return new UsuarioTokenDto
            {
                Usuario = _mapper.Map<UsuarioDto>(usuario),
                Token = newToken,
                RefreshToken = newRefreshToken,
                DataExpiracao = DateTime.UtcNow.AddHours(_jwtSettings.ExpireHours)
            };
        }

        public async Task RevokeToken(string refreshToken)
        {
            var token = await _refreshTokenRepository.GetByTokenAsync(refreshToken);
            if (token == null || token.IsExpired)
                throw new DomainException("Refresh token inválido");

            token.Revoked = DateTime.UtcNow;
            await _refreshTokenRepository.UpdateAsync(token);
        }

        public async Task RequestPasswordReset(string email)
        {
            var usuario = await _usuarioRepository.GetByEmailAsync(email);
            if (usuario == null) return;

            usuario.PasswordResetToken = GenerateToken();
            usuario.PasswordResetTokenExpires = DateTime.UtcNow.AddHours(2);
            await _usuarioRepository.UpdateAsync(usuario);

            var resetLink = $"{_configuration["ClientApp:Url"]}/resetar-senha?token={usuario.PasswordResetToken}";
            await _emailService.SendPasswordResetEmailAsync(email, resetLink);
        }

        public async Task ConfirmEmail(string token)
        {
            var usuario = await _usuarioRepository.GetByConfirmationTokenAsync(token);
            if (usuario == null)
                throw new DomainException("Token de confirmação inválido");

            usuario.EmailConfirmado = true;
            usuario.EmailConfirmationToken = null;
            await _usuarioRepository.UpdateAsync(usuario);
        }

        public async Task ResetPassword(string token, string newPassword)
        {
            var usuario = await _usuarioRepository.GetByPasswordResetTokenAsync(token);
            if (usuario == null || usuario.PasswordResetTokenExpires < DateTime.UtcNow)
                throw new DomainException("Token de redefinição inválido ou expirado");

            usuario.SenhaHash = CreatePasswordHash(newPassword);
            usuario.PasswordResetToken = null;
            usuario.PasswordResetTokenExpires = null;
            await _usuarioRepository.UpdateAsync(usuario);
        }
    }

    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpireHours { get; set; }
    }
}