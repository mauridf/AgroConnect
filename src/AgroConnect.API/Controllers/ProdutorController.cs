using AgroConnect.Application.Dtos;
using AgroConnect.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgroConnect.API.Controllers
{
    [ApiController]
    [Route("api/produtor")]
    public class ProdutorController : ControllerBase
    {
        private readonly IProdutorRuralService _service;

        public ProdutorController(IProdutorRuralService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<ProdutorRuralDto>> Create([FromBody] CreateProdutorRuralDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutorRuralDto>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("uf/{uf}")]
        public async Task<ActionResult<IEnumerable<ProdutorRuralDto>>> GetByUf(string uf)
        {
            var result = await _service.GetByUfAsync(uf);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutorRuralDto>> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("cpf/{cpf}")]
        public async Task<ActionResult<ProdutorRuralDto>> GetByCpf(string cpf)
        {
            var result = await _service.GetByCpfAsync(cpf);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<ProdutorRuralDto>> Update([FromBody] UpdateProdutorRuralDto dto)
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
