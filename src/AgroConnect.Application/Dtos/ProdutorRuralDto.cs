using AgroConnect.Domain.ValueObjects;

namespace AgroConnect.Application.Dtos
{
    public class ProdutorRuralDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public Endereco Endereco { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
    }

    public class CreateProdutorRuralDto
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public Endereco Endereco { get; set; }
    }

    public class UpdateProdutorRuralDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public Endereco Endereco { get; set; }
    }

    public class ProdutorRuralSummaryDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string UF { get; set; }
        public int TotalFazendas { get; set; }
    }
}