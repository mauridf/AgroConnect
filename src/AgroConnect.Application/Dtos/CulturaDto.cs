using AgroConnect.Domain.Enums;

namespace AgroConnect.Application.Dtos
{
    public class CulturaDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public CategoriaCultura Categoria { get; set; }
        public string TempoColheita { get; set; }
        public ExigenciaClimatica ExigenciaClimatica { get; set; }
        public string Detalhes { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
    }

    public class CreateCulturaDto
    {
        public string Nome { get; set; }
        public CategoriaCultura Categoria { get; set; }
        public string TempoColheita { get; set; }
        public ExigenciaClimatica ExigenciaClimatica { get; set; }
        public string Detalhes { get; set; }
    }

    public class UpdateCulturaDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public CategoriaCultura Categoria { get; set; }
        public string TempoColheita { get; set; }
        public ExigenciaClimatica ExigenciaClimatica { get; set; }
        public string Detalhes { get; set; }
    }

    public class CulturaSummaryDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public CategoriaCultura Categoria { get; set; }
        public decimal AreaUtilizadaHectares { get; set; }
    }
}