using AgroConnect.Application.Dtos;

namespace AgroConnect.Application.Interfaces
{
    public interface IDashboardRepository
    {
        Task<int> GetTotalFazendasAsync();
        Task<IEnumerable<UfSummaryDto>> GetTotalFazendasPorUfAsync();
        Task<decimal> GetTotalHectaresAsync();
        Task<IEnumerable<UfSummaryDto>> GetTotalHectaresPorUfAsync();
        Task<decimal> GetTotalHectaresAgricultaveisAsync();
        Task<IEnumerable<UfSummaryDto>> GetTotalHectaresAgricultaveisPorUfAsync();
        Task<int> GetTotalProdutoresAsync();
        Task<IEnumerable<UfSummaryDto>> GetTotalProdutoresPorUfAsync();
        Task<int> GetTotalCulturasPlantadasAsync();
        Task<IEnumerable<CulturaSummaryDashboardDto>> GetTotalCulturasPlantadasPorCulturaAsync();
        Task<IEnumerable<UfSummaryDto>> GetTotalCulturasPlantadasPorUfAsync();
        Task<IEnumerable<CategoriaSummaryDashboardDto>> GetTotalCulturasPlantadasPorCategoriaAsync();
        Task<IEnumerable<CategoriaSummaryDashboardDto>> GetTotalCulturasPlantadasPorCategoriaPorUfAsync(string uf);
        Task<DashboardSummaryDto> GetResumoGeralAsync();
    }
}
