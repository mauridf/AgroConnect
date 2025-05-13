using AgroConnect.Application.Dtos;
using AgroConnect.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgroConnect.API.Controllers
{
    [ApiController]
    [Route("api/dashboard")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _service;

        public DashboardController(IDashboardService service)
        {
            _service = service;
        }

        [HttpGet("resumo-geral")]
        public async Task<ActionResult<DashboardSummaryDto>> GetResumoGeral()
        {
            return Ok(await _service.GetResumoGeralAsync());
        }

        [HttpGet("total-fazendas")]
        public async Task<ActionResult<int>> GetTotalFazendas()
        {
            return Ok(await _service.GetTotalFazendasAsync());
        }

        [HttpGet("total-fazendas-por-uf")]
        public async Task<ActionResult<IEnumerable<UfSummaryDto>>> GetTotalFazendasPorUf()
        {
            return Ok(await _service.GetTotalFazendasPorUfAsync());
        }

        [HttpGet("total-hectares")]
        public async Task<ActionResult<decimal>> GetTotalHectares()
        {
            return Ok(await _service.GetTotalHectaresAsync());
        }

        [HttpGet("total-hectares-por-uf")]
        public async Task<ActionResult<IEnumerable<UfSummaryDto>>> GetTotalHectaresPorUf()
        {
            return Ok(await _service.GetTotalHectaresPorUfAsync());
        }

        [HttpGet("total-hectares-agricultavel")]
        public async Task<ActionResult<decimal>> GetTotalHectaresAgricultaveis()
        {
            return Ok(await _service.GetTotalHectaresAgricultaveisAsync());
        }

        [HttpGet("total-hectares-agricultavel-por-uf")]
        public async Task<ActionResult<IEnumerable<UfSummaryDto>>> GetTotalHectaresAgricultaveisPorUf()
        {
            return Ok(await _service.GetTotalHectaresAgricultaveisPorUfAsync());
        }

        [HttpGet("total-produtores")]
        public async Task<ActionResult<int>> GetTotalProdutores()
        {
            return Ok(await _service.GetTotalProdutoresAsync());
        }

        [HttpGet("total-produtores-por-uf")]
        public async Task<ActionResult<IEnumerable<UfSummaryDto>>> GetTotalProdutoresPorUf()
        {
            return Ok(await _service.GetTotalProdutoresPorUfAsync());
        }

        [HttpGet("total-culturas-plantadas")]
        public async Task<ActionResult<int>> GetTotalCulturasPlantadas()
        {
            return Ok(await _service.GetTotalCulturasPlantadasAsync());
        }

        [HttpGet("total-culturas-plantadas-por-cultura")]
        public async Task<ActionResult<IEnumerable<CulturaSummaryDashboardDto>>> GetTotalCulturasPlantadasPorCultura()
        {
            return Ok(await _service.GetTotalCulturasPlantadasPorCulturaAsync());
        }

        [HttpGet("total-culturas-plantadas-por-uf")]
        public async Task<ActionResult<IEnumerable<UfSummaryDto>>> GetTotalCulturasPlantadasPorUf()
        {
            return Ok(await _service.GetTotalCulturasPlantadasPorUfAsync());
        }

        [HttpGet("total-culturas-plantadas-por-categoria")]
        public async Task<ActionResult<IEnumerable<CategoriaSummaryDashboardDto>>> GetTotalCulturasPlantadasPorCategoria()
        {
            return Ok(await _service.GetTotalCulturasPlantadasPorCategoriaAsync());
        }

        [HttpGet("total-culturas-plantadas-por-categoria-por-uf/{uf}")]
        public async Task<ActionResult<IEnumerable<CategoriaSummaryDashboardDto>>> GetTotalCulturasPlantadasPorCategoriaPorUf(string uf)
        {
            return Ok(await _service.GetTotalCulturasPlantadasPorCategoriaPorUfAsync(uf));
        }
    }
}
