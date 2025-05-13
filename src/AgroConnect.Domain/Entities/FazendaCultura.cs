using AgroConnect.Domain.Enums;

namespace AgroConnect.Domain.Entities
{
    public class FazendaCultura : EntityBase
    {
        protected FazendaCultura() { }
        public Guid FazendaId { get; set; }
        public Guid CulturaId { get; set; }
        public decimal AreaUtilizadaHectares { get; set; }

        // Navigation properties
        public Fazenda Fazenda { get; set; }
        public Cultura Cultura { get; set; }

        public FazendaCultura(Guid fazendaId, Guid culturaId, decimal areaUtilizada)
        {
            FazendaId = fazendaId;
            CulturaId = culturaId;
            AreaUtilizadaHectares = areaUtilizada;
            Validate();
        }

        // Método para atualização
        public void Update(Guid fazendaId, Guid culturaId, decimal areaUtilizada)
        {
            FazendaId = fazendaId;
            CulturaId = culturaId;
            AreaUtilizadaHectares = areaUtilizada;
            UpdateTimestamp();

            Validate();
        }

        private void Validate()
        {
            if (AreaUtilizadaHectares <= 0)
                throw new DomainException("A Área Utilizada não pode ser menor ou igual a Zero(0)");

            if (Fazenda != null && AreaUtilizadaHectares > Fazenda.AreaAgricultavelHectares)
                throw new DomainException("A Área Utilizada não pode ser maior do que a Área Agricultável");
        }
    }
}