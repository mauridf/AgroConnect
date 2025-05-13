using AgroConnect.Application.Dtos;
using AgroConnect.Application.Interfaces;

namespace AgroConnect.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _repository;

        public DashboardService(IDashboardRepository repository)
        {
            _repository = repository;
        }

        public async Task<DashboardSummaryDto> GetResumoGeralAsync()
        {
            return await _repository.GetResumoGeralAsync();
        }

        public async Task<int> GetTotalFazendasAsync()
        {
            return await _repository.GetTotalFazendasAsync();
        }

        public async Task<IEnumerable<UfSummaryDto>> GetTotalFazendasPorUfAsync()
        {
            return await _repository.GetTotalFazendasPorUfAsync();
        }

        public async Task<decimal> GetTotalHectaresAsync()
        {
            return await _repository.GetTotalHectaresAsync();
        }

        public async Task<IEnumerable<UfSummaryDto>> GetTotalHectaresPorUfAsync()
        {
            return await _repository.GetTotalHectaresPorUfAsync();
        }

        public async Task<decimal> GetTotalHectaresAgricultaveisAsync()
        {
            return await _repository.GetTotalHectaresAgricultaveisAsync();
        }

        public async Task<IEnumerable<UfSummaryDto>> GetTotalHectaresAgricultaveisPorUfAsync()
        {
            return await _repository.GetTotalHectaresAgricultaveisPorUfAsync();
        }

        public async Task<int> GetTotalProdutoresAsync()
        {
            return await _repository.GetTotalProdutoresAsync();
        }

        public async Task<IEnumerable<UfSummaryDto>> GetTotalProdutoresPorUfAsync()
        {
            return await _repository.GetTotalProdutoresPorUfAsync();
        }

        public async Task<int> GetTotalCulturasPlantadasAsync()
        {
            return await _repository.GetTotalCulturasPlantadasAsync();
        }

        public async Task<IEnumerable<CulturaSummaryDashboardDto>> GetTotalCulturasPlantadasPorCulturaAsync()
        {
            return await _repository.GetTotalCulturasPlantadasPorCulturaAsync();
        }

        public async Task<IEnumerable<UfSummaryDto>> GetTotalCulturasPlantadasPorUfAsync()
        {
            return await _repository.GetTotalCulturasPlantadasPorUfAsync();
        }

        public async Task<IEnumerable<CategoriaSummaryDashboardDto>> GetTotalCulturasPlantadasPorCategoriaAsync()
        {
            return await _repository.GetTotalCulturasPlantadasPorCategoriaAsync();
        }

        public async Task<IEnumerable<CategoriaSummaryDashboardDto>> GetTotalCulturasPlantadasPorCategoriaPorUfAsync(string uf)
        {
            return await _repository.GetTotalCulturasPlantadasPorCategoriaPorUfAsync(uf);
        }
    }
}
