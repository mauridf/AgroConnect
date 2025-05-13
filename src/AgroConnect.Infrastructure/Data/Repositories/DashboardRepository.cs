using AgroConnect.Application.Dtos;
using AgroConnect.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AgroConnect.Infrastructure.Data.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly AgroConnectDbContext _context;

        public DashboardRepository(AgroConnectDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetTotalFazendasAsync()
        {
            return await _context.Fazendas.CountAsync();
        }

        public async Task<IEnumerable<UfSummaryDto>> GetTotalFazendasPorUfAsync()
        {
            return await _context.Fazendas
                .GroupBy(f => f.Endereco.UF)
                .Select(g => new UfSummaryDto
                {
                    UF = g.Key,
                    Quantidade = g.Count(),
                    Hectares = g.Sum(f => f.AreaTotalHectares)
                })
                .ToListAsync();
        }

        public async Task<decimal> GetTotalHectaresAsync()
        {
            return await _context.Fazendas.SumAsync(f => f.AreaTotalHectares);
        }

        public async Task<IEnumerable<UfSummaryDto>> GetTotalHectaresPorUfAsync()
        {
            return await _context.Fazendas
                .GroupBy(f => f.Endereco.UF)
                .Select(g => new UfSummaryDto
                {
                    UF = g.Key,
                    Hectares = g.Sum(f => f.AreaTotalHectares)
                })
                .ToListAsync();
        }

        public async Task<decimal> GetTotalHectaresAgricultaveisAsync()
        {
            return await _context.Fazendas.SumAsync(f => f.AreaAgricultavelHectares);
        }

        public async Task<IEnumerable<UfSummaryDto>> GetTotalHectaresAgricultaveisPorUfAsync()
        {
            return await _context.Fazendas
                .GroupBy(f => f.Endereco.UF)
                .Select(g => new UfSummaryDto
                {
                    UF = g.Key,
                    Hectares = g.Sum(f => f.AreaAgricultavelHectares)
                })
                .ToListAsync();
        }

        public async Task<int> GetTotalProdutoresAsync()
        {
            return await _context.ProdutoresRurais.CountAsync();
        }

        public async Task<IEnumerable<UfSummaryDto>> GetTotalProdutoresPorUfAsync()
        {
            return await _context.ProdutoresRurais
                .GroupBy(p => p.Endereco.UF)
                .Select(g => new UfSummaryDto
                {
                    UF = g.Key,
                    Quantidade = g.Count()
                })
                .ToListAsync();
        }

        public async Task<int> GetTotalCulturasPlantadasAsync()
        {
            return await _context.FazendaCulturas.CountAsync();
        }

        public async Task<IEnumerable<CulturaSummaryDashboardDto>> GetTotalCulturasPlantadasPorCulturaAsync()
        {
            return await _context.FazendaCulturas
                .Include(fc => fc.Cultura)
                .GroupBy(fc => fc.Cultura.Nome)
                .Select(g => new CulturaSummaryDashboardDto
                {
                    CulturaNome = g.Key,
                    Quantidade = g.Count(),
                    AreaTotal = g.Sum(fc => fc.AreaUtilizadaHectares)
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<UfSummaryDto>> GetTotalCulturasPlantadasPorUfAsync()
        {
            return await _context.FazendaCulturas
                .Include(fc => fc.Fazenda)
                .GroupBy(fc => fc.Fazenda.Endereco.UF)
                .Select(g => new UfSummaryDto
                {
                    UF = g.Key,
                    Quantidade = g.Count(),
                    Hectares = g.Sum(fc => fc.AreaUtilizadaHectares)
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<CategoriaSummaryDashboardDto>> GetTotalCulturasPlantadasPorCategoriaAsync()
        {
            return await _context.FazendaCulturas
                .Include(fc => fc.Cultura)
                .GroupBy(fc => fc.Cultura.Categoria)
                .Select(g => new CategoriaSummaryDashboardDto
                {
                    Categoria = g.Key,
                    Quantidade = g.Count(),
                    AreaTotal = g.Sum(fc => fc.AreaUtilizadaHectares)
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<CategoriaSummaryDashboardDto>> GetTotalCulturasPlantadasPorCategoriaPorUfAsync(string uf)
        {
            return await _context.FazendaCulturas
                .Include(fc => fc.Fazenda)
                .Include(fc => fc.Cultura)
                .Where(fc => fc.Fazenda.Endereco.UF == uf)
                .GroupBy(fc => fc.Cultura.Categoria)
                .Select(g => new CategoriaSummaryDashboardDto
                {
                    Categoria = g.Key,
                    Quantidade = g.Count(),
                    AreaTotal = g.Sum(fc => fc.AreaUtilizadaHectares)
                })
                .ToListAsync();
        }

        public async Task<DashboardSummaryDto> GetResumoGeralAsync()
        {
            return new DashboardSummaryDto
            {
                TotalProdutores = await GetTotalProdutoresAsync(),
                TotalFazendas = await GetTotalFazendasAsync(),
                TotalHectares = await GetTotalHectaresAsync(),
                TotalHectaresAgricultaveis = await GetTotalHectaresAgricultaveisAsync(),
                TotalCulturasPlantadas = await GetTotalCulturasPlantadasAsync()
            };
        }
    }
}
