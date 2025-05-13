using AgroConnect.Application.Dtos;
using AgroConnect.Application.Interfaces;
using AgroConnect.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace AgroConnect.API.Controllers
{
    [ApiController]
    [Route("api/cultura")]
    public class CulturaController : ControllerBase
    {
        private readonly ICulturaService _service;

        public CulturaController(ICulturaService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<CulturaDto>> Create([FromBody] CreateCulturaDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CulturaDto>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("categoria/{categoria}")]
        public async Task<ActionResult<IEnumerable<CulturaDto>>> GetByCategoria(CategoriaCultura categoria)
        {
            var result = await _service.GetByCategoriaAsync(categoria);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CulturaDto>> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<CulturaDto>> Update([FromBody] UpdateCulturaDto dto)
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
