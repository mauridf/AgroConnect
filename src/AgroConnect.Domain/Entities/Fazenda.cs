using AgroConnect.Domain.ValueObjects;

namespace AgroConnect.Domain.Entities
{
    public class Fazenda : EntityBase
    {
        protected Fazenda() { }
        public Guid ProdutorId { get; set; }
        public string Nome { get; set; }
        public string? CNPJ { get; set; }
        public Endereco Endereco { get; set; }
        public decimal AreaTotalHectares { get; set; }
        public decimal AreaAgricultavelHectares { get; set; }
        public decimal AreaVegetacaoHectares { get; set; }
        public decimal AreaConstruidaHectares { get; set; }
        
        // Navigation properties
        public ProdutorRural Produtor { get; set; }
        public ICollection<FazendaCultura> FazendaCulturas { get; set; }

        public Fazenda(Guid produtorId, string nomeFazenda, string? cnpj, Endereco endereco, decimal areaTotal, decimal areaAgricultavel,
            decimal areaVegetacao, decimal areaConstruida)
        {
            ProdutorId = produtorId;
            Nome = nomeFazenda;
            CNPJ = cnpj;
            Endereco = endereco;
            AreaTotalHectares = areaTotal;
            AreaAgricultavelHectares = areaAgricultavel;
            AreaVegetacaoHectares = areaVegetacao;
            AreaConstruidaHectares = areaConstruida;
            Validate();
        }

        // Método para atualização
        public void Update(Guid produtorId, string nomeFazenda, string? cnpj, Endereco endereco, decimal areaTotal, decimal areaAgricultavel,
            decimal areaVegetacao, decimal areaConstruida)
        {
            ProdutorId = produtorId;
            Nome = nomeFazenda;
            CNPJ = cnpj;
            Endereco = endereco;
            AreaTotalHectares = areaTotal;
            AreaAgricultavelHectares = areaAgricultavel;
            AreaVegetacaoHectares = areaVegetacao;
            AreaConstruidaHectares = areaConstruida;
            UpdateTimestamp();

            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Nome))
                throw new DomainException("Nome da Fazenda é Obrigatório");

            if (AreaTotalHectares <= 0)
                throw new DomainException("A Área Total em Hectares deve ser maior do que Zero (0)");

            if (AreaAgricultavelHectares <= 0)
                throw new DomainException("A Área Agricultável deve ser maior do que Zero (0)");

            if (AreaAgricultavelHectares > AreaTotalHectares)
                throw new DomainException("A Área Agricultável não pode ser maior do que a Área Total");

            if (AreaVegetacaoHectares <= 0)
                throw new DomainException("A Área de Vegetação deve ser maior do que Zero (0)");

            if (AreaVegetacaoHectares > AreaTotalHectares)
                throw new DomainException("A Área de Vegetação não pode ser maior do que a Área Total");

            if (AreaConstruidaHectares <= 0)
                throw new DomainException("A Área Construida deve ser maior do que Zero (0)");

            if (AreaConstruidaHectares > AreaTotalHectares)
                throw new DomainException("A Área Construida não pode ser maior do que a Área Total");
        }
    }
}