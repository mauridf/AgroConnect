using AgroConnect.Application.Dtos;

public class UsuarioTokenDto
{
    public UsuarioDto Usuario { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public DateTime DataExpiracao { get; set; }
}