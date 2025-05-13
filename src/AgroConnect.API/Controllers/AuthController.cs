using AgroConnect.Application.Dtos;
using AgroConnect.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AgroConnect.API.Controllers
{
    [Route("api/autenticacao")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UsuarioTokenDto>> Login(UsuarioLoginDto usuarioLogin)
        {
            try
            {
                var result = await _authService.Login(usuarioLogin);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<UsuarioTokenDto>> Register(UsuarioRegisterDto usuarioRegister)
        {
            try
            {
                var result = await _authService.Register(usuarioRegister);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<UsuarioTokenDto>> RefreshToken(RefreshTokenRequest request)
        {
            try
            {
                var result = await _authService.RefreshToken(request.Token, request.RefreshToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken(RevokeTokenRequest request)
        {
            try
            {
                await _authService.RevokeToken(request.RefreshToken);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("request-password-reset")]
        public async Task<IActionResult> RequestPasswordReset([FromBody] string email)
        {
            try
            {
                await _authService.RequestPasswordReset(email);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            try
            {
                await _authService.ResetPassword(request.Token, request.NewPassword);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string token)
        {
            try
            {
                await _authService.ConfirmEmail(token);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}