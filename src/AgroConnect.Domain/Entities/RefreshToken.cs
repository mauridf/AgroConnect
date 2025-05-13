namespace AgroConnect.Domain.Entities
{
    public class RefreshToken : EntityBase
    {
        public RefreshToken() { }
        public Guid UsuarioId { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public DateTime? Revoked { get; set; }
        public bool IsActive => !IsExpired && Revoked == null;

        // Navigation property
        public Usuario Usuario { get; set; }
    }
}
