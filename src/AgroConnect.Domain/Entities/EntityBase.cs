namespace AgroConnect.Domain.Entities;

public class EntityBase
{
    public Guid Id { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataAtualizacao { get; set; }

    protected EntityBase()
    {
        Id = Guid.NewGuid();
        DataCriacao = DateTime.UtcNow;
    }

    public void UpdateTimestamp()
    {
        DataAtualizacao = DateTime.UtcNow;
    }

    public class DomainException : Exception
    {
        public DomainException(string message) : base(message) { }
    }
}
