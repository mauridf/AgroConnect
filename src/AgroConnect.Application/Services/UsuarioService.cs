using AgroConnect.Application.Dtos;
using AgroConnect.Application.Interfaces;
using AgroConnect.Domain.Entities;
using AutoMapper;
using static AgroConnect.Domain.Entities.EntityBase;

namespace AgroConnect.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public UsuarioService(
            IUsuarioRepository usuarioRepository,
            IMapper mapper,
            IAuthService authService)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _authService = authService;
        }

        public async Task<UsuarioDto> GetByIdAsync(Guid id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
                throw new DomainException("Usuário não encontrado");

            return _mapper.Map<UsuarioDto>(usuario);
        }

        public async Task<UsuarioDto> GetByNomeUsuarioAsync(string nomeUsuario)
        {
            var usuario = await _usuarioRepository.GetByNomeUsuarioAsync(nomeUsuario);
            if (usuario == null)
                throw new DomainException("Usuário não encontrado");

            return _mapper.Map<UsuarioDto>(usuario);
        }

        public async Task<UsuarioDto> CreateAsync(UsuarioRegisterDto usuarioRegister)
        {
            if (await NomeUsuarioExistsAsync(usuarioRegister.NomeUsuario))
                throw new DomainException("Nome de usuário já está em uso");

            var usuario = new Usuario(
                usuarioRegister.NomeUsuario,
                _authService.CreatePasswordHash(usuarioRegister.Senha),
                usuarioRegister.TipoUsuario);

            await _usuarioRepository.AddAsync(usuario);

            return _mapper.Map<UsuarioDto>(usuario);
        }

        public async Task<UsuarioDto> UpdateAsync(Guid id, UsuarioUpdateDto usuarioUpdate)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
                throw new DomainException("Usuário não encontrado");

            if (!string.IsNullOrEmpty(usuarioUpdate.NovaSenha))
            {
                usuario.SenhaHash = _authService.CreatePasswordHash(usuarioUpdate.NovaSenha);
            }

            usuario.Update(usuarioUpdate.NomeUsuario, usuario.SenhaHash, usuarioUpdate.TipoUsuario);
            await _usuarioRepository.UpdateAsync(usuario);

            return _mapper.Map<UsuarioDto>(usuario);
        }

        public async Task DeleteAsync(Guid id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
                throw new DomainException("Usuário não encontrado");

            await _usuarioRepository.DeleteAsync(usuario);
        }

        public async Task<bool> NomeUsuarioExistsAsync(string nomeUsuario)
        {
            return await _usuarioRepository.NomeUsuarioExistsAsync(nomeUsuario);
        }
    }
}