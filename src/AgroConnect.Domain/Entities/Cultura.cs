using AgroConnect.Domain.Enums;
using AgroConnect.Domain.ValueObjects;

namespace AgroConnect.Domain.Entities
{
    public class Cultura : EntityBase
    {
        public string Nome { get; set; }
        public CategoriaCultura Categoria { get; set; }
        public string? TempoColheita { get; set; }
        public ExigenciaClimatica ExigenciaClimatica { get; set; }
        public string? Detalhes { get; set; }

        // Navigation properties
        public ICollection<FazendaCultura> FazendaCulturas { get; set; }

        public Cultura(string nomeCultura, CategoriaCultura categoria, string tempoColheita, ExigenciaClimatica exigenciaClimatica,
            string detalhes)
        {
            Nome = nomeCultura;
            Categoria = categoria;
            TempoColheita = tempoColheita;
            ExigenciaClimatica = exigenciaClimatica;
            Detalhes = detalhes;
            Validate();
        }

        // Método para atualização
        public void Update(string nomeCultura, CategoriaCultura categoria, string tempoColheita, ExigenciaClimatica exigenciaClimatica,
            string detalhes)
        {
            Nome = nomeCultura;
            Categoria = categoria;
            TempoColheita = tempoColheita;
            ExigenciaClimatica = exigenciaClimatica;
            Detalhes = detalhes;
            UpdateTimestamp();

            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Nome))
                throw new DomainException("Nome da Fazenda é Obrigatório");
        }
    }
}