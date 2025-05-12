using AgroConnect.Domain.Enums;
using AgroConnect.Domain.ValueObjects;

namespace AgroConnect.Domain.Entities
{
    public class ProdutorRural : EntityBase
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string? Email { get; set; }
        public string Telefone { get; set; }
        public Endereco Endereco { get; set; }

        // Navigation properties
        public ICollection<Fazenda> Fazendas { get; set; }

        public ProdutorRural(string nomeProdutor, string cpf, string email, string telefone, Endereco endereco)
        {
            Nome = nomeProdutor;
            CPF = cpf;
            Email = email;
            Telefone = telefone;
            Endereco = endereco;

            Validate();
        }

        // Método para atualização
        public void Update(string nomeProdutor, string cpf, string email, string telefone, Endereco endereco)
        {
            Nome = nomeProdutor;
            CPF = cpf;
            Email = email;
            Telefone = telefone;
            Endereco = endereco;
            UpdateTimestamp();

            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Nome))
                throw new DomainException("Nome do Produtor Rural é Obrigatório");

            if (string.IsNullOrWhiteSpace(CPF))
                throw new DomainException("O CPF do Produtor Rural é Obrigatório");

            if (string.IsNullOrEmpty(Telefone))
                throw new DomainException("O Telefone do Produtor Rural é Obrigatório");

            //if (string.IsNullOrEmpty(Endereco))
            //    throw new DomainException("O Endereço do Produtor Rural é Obrigatório");
        }
    }
}