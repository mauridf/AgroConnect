using AgroConnect.Application.Dtos;
using AgroConnect.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static AgroConnect.Domain.Entities.EntityBase;

namespace AgroConnect.API.Controllers
{
    [Authorize]
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDto>> GetById(Guid id)
        {
            try
            {
                var usuario = await _usuarioService.GetByIdAsync(id);
                return Ok(usuario);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("nome/{nomeUsuario}")]
        public async Task<ActionResult<UsuarioDto>> GetByNomeUsuario(string nomeUsuario)
        {
            try
            {
                var usuario = await _usuarioService.GetByNomeUsuarioAsync(nomeUsuario);
                return Ok(usuario);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<UsuarioDto>> Create(UsuarioRegisterDto usuarioRegister)
        {
            try
            {
                var usuario = await _usuarioService.CreateAsync(usuarioRegister);
                return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioDto>> Update(Guid id, UsuarioUpdateDto usuarioUpdate)
        {
            try
            {
                var usuario = await _usuarioService.UpdateAsync(id, usuarioUpdate);
                return Ok(usuario);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _usuarioService.DeleteAsync(id);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}