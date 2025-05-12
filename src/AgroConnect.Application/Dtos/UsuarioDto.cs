using AgroConnect.Domain.Enums;

namespace AgroConnect.Application.Dtos
{
    public class UsuarioDto
    {
        public Guid Id { get; set; }
        public string NomeUsuario { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
    }

    public class UsuarioLoginDto
    {
        public string NomeUsuario { get; set; }
        public string Senha { get; set; }
    }

    public class UsuarioRegisterDto
    {
        public string NomeUsuario { get; set; }
        public string Senha { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
    }

    public class UsuarioTokenDto
    {
        public UsuarioDto Usuario { get; set; }
        public string Token { get; set; }
        public DateTime DataExpiracao { get; set; }
    }
}