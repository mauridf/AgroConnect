namespace AgroConnect.Application.Dtos
{
    public class FazendaCulturaDto
    {
        public Guid Id { get; set; }
        public Guid FazendaId { get; set; }
        public Guid CulturaId { get; set; }
        public decimal AreaUtilizadaHectares { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public CulturaDto Cultura { get; set; }
    }

    public class CreateFazendaCulturaDto
    {
        public Guid FazendaId { get; set; }
        public Guid CulturaId { get; set; }
        public decimal AreaUtilizadaHectares { get; set; }
    }

    public class UpdateFazendaCulturaDto
    {
        public Guid Id { get; set; }
        public Guid FazendaId { get; set; }
        public Guid CulturaId { get; set; }
        public decimal AreaUtilizadaHectares { get; set; }
    }
}