using AgroConnect.Application.Dtos;
using AgroConnect.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AgroConnect.API.Controllers
{
    [ApiController]
    [Route("api/fazendacultura")]
    public class FazendaCulturaController : ControllerBase
    {
        private readonly IFazendaCulturaService _service;

        public FazendaCulturaController(IFazendaCulturaService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<FazendaCulturaDto>> Create([FromBody] CreateFazendaCulturaDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet("fazenda/{fazendaId}")]
        public async Task<ActionResult<IEnumerable<FazendaCulturaDto>>> GetByFazendaId(Guid fazendaId)
        {
            var result = await _service.GetByFazendaIdAsync(fazendaId);
            return Ok(result);
        }

        [HttpGet("areautilizada/{fazendaId}")]
        public async Task<ActionResult<decimal>> GetAreaTotalUtilizada(Guid fazendaId)
        {
            var result = await _service.GetAreaTotalUtilizadaAsync(fazendaId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FazendaCulturaDto>> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<FazendaCulturaDto>> Update([FromBody] UpdateFazendaCulturaDto dto)
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
