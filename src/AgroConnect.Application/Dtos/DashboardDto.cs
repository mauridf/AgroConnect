using AgroConnect.Domain.Enums;

namespace AgroConnect.Application.Dtos
{
    public class DashboardSummaryDto
    {
        public int TotalProdutores { get; set; }
        public int TotalFazendas { get; set; }
        public decimal TotalHectares { get; set; }
        public decimal TotalHectaresAgricultaveis { get; set; }
        public int TotalCulturasPlantadas { get; set; }
    }

    public class UfSummaryDto
    {
        public string UF { get; set; }
        public int Quantidade { get; set; }
        public decimal Hectares { get; set; }
    }

    public class CulturaSummaryDashboardDto
    {
        public string CulturaNome { get; set; }
        public int Quantidade { get; set; }
        public decimal AreaTotal { get; set; }
    }

    public class CategoriaSummaryDashboardDto
    {
        public CategoriaCultura Categoria { get; set; }
        public int Quantidade { get; set; }
        public decimal AreaTotal { get; set; }
    }
}