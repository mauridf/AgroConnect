using AgroConnect.Domain.ValueObjects;

namespace AgroConnect.Application.Dtos
{
    public class FazendaDto
    {
        public Guid Id { get; set; }
        public Guid ProdutorId { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public Endereco Endereco { get; set; }
        public decimal AreaTotalHectares { get; set; }
        public decimal AreaAgricultavelHectares { get; set; }
        public decimal AreaVegetacaoHectares { get; set; }
        public decimal AreaConstruidaHectares { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public List<CulturaSummaryDto> Culturas { get; set; } = new();
    }

    public class CreateFazendaDto
    {
        public Guid ProdutorId { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public Endereco Endereco { get; set; }
        public bool UsarEnderecoProdutor { get; set; }
        public decimal AreaTotalHectares { get; set; }
        public decimal AreaAgricultavelHectares { get; set; }
        public decimal AreaVegetacaoHectares { get; set; }
        public decimal AreaConstruidaHectares { get; set; }
    }

    public class UpdateFazendaDto
    {
        public Guid Id { get; set; }
        public Guid ProdutorId { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public Endereco Endereco { get; set; }
        public bool UsarEnderecoProdutor { get; set; }
        public decimal AreaTotalHectares { get; set; }
        public decimal AreaAgricultavelHectares { get; set; }
        public decimal AreaVegetacaoHectares { get; set; }
        public decimal AreaConstruidaHectares { get; set; }
    }

    public class FazendaSummaryDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string UF { get; set; }
        public decimal AreaTotalHectares { get; set; }
    }
}