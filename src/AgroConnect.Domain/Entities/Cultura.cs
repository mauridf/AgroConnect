using AgroConnect.Domain.Enums;

namespace AgroConnect.Domain.Entities
{
    public class Cultura : EntityBase
    {
        // Construtor sem parâmetros (necessário para o EF Core)
        protected Cultura() { }

        // Seu construtor principal
        public Cultura(string nome, CategoriaCultura categoria, string tempoColheita,
                      ExigenciaClimatica exigenciaClimatica, string detalhes)
        {
            Nome = nome;
            Categoria = categoria;
            TempoColheita = tempoColheita;
            ExigenciaClimatica = exigenciaClimatica;
            Detalhes = detalhes;

            Validate();
        }

        public string Nome { get; private set; }
        public CategoriaCultura Categoria { get; private set; }
        public string TempoColheita { get; private set; }
        public ExigenciaClimatica ExigenciaClimatica { get; private set; }
        public string Detalhes { get; private set; }

        // Navigation property
        public ICollection<FazendaCultura> FazendaCulturas { get; set; }

        public void Atualizar(
        string nome,
        CategoriaCultura categoria,
        string tempoColheita,
        ExigenciaClimatica exigenciaClimatica,
        string detalhes)
        {
            Nome = nome;
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
                throw new DomainException("Nome da cultura é obrigatório");
        }
    }
}