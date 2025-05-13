using AgroConnect.Domain.Enums;
using AgroConnect.Domain.ValueObjects;

namespace AgroConnect.Domain.Entities
{
    public class Usuario : EntityBase
    {
        public string NomeUsuario { get; set; }
        public string SenhaHash { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmado { get; set; }
        public string PasswordResetToken { get; set; }
        public DateTime? PasswordResetTokenExpires { get; set; }
        public string EmailConfirmationToken { get; set; }

        public Usuario(string nomeUsuario, string senhaHash, TipoUsuario tipoUsuario, string email = null, bool emailConfirmado = false)
        {
            NomeUsuario = nomeUsuario;
            SenhaHash = senhaHash;
            TipoUsuario = tipoUsuario;
            Email = email;
            EmailConfirmado = emailConfirmado;

            Validate();
        }

        // Método para atualização
        public void Update(string nomeUsuario, string senhaHash, TipoUsuario tipoUsuario, string email = null, bool emailConfirmado = false)
        {
            NomeUsuario = nomeUsuario;
            SenhaHash = senhaHash;
            TipoUsuario = tipoUsuario;
            Email = email;
            EmailConfirmado = emailConfirmado;
            UpdateTimestamp();

            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(NomeUsuario))
                throw new DomainException("Nome de usuário é obrigatório");

            if (string.IsNullOrWhiteSpace(SenhaHash))
                throw new DomainException("Senha é obrigatória");
        }
    }
}