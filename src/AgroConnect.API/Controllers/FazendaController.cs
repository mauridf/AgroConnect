using AgroConnect.Application.Dtos;
using AgroConnect.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgroConnect.API.Controllers
{
    [ApiController]
    [Route("api/fazenda")]
    public class FazendaController : ControllerBase
    {
        private readonly IFazendaService _service;

        public FazendaController(IFazendaService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<FazendaDto>> Create([FromBody] CreateFazendaDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FazendaDto>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("uf/{uf}")]
        public async Task<ActionResult<IEnumerable<FazendaDto>>> GetByUf(string uf)
        {
            var result = await _service.GetByUfAsync(uf);
            return Ok(result);
        }

        [HttpGet("produtor/{produtorId}")]
        public async Task<ActionResult<IEnumerable<FazendaDto>>> GetByProdutorId(Guid produtorId)
        {
            var result = await _service.GetByProdutorIdAsync(produtorId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FazendaDto>> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("cnpj/{cnpj}")]
        public async Task<ActionResult<FazendaDto>> GetByCnpj(string cnpj)
        {
            var result = await _service.GetByCnpjAsync(cnpj);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<FazendaDto>> Update([FromBody] UpdateFazendaDto dto)
        {
            var result = await _service.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
