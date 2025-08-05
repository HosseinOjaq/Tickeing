namespace Ticketing.Application.Models.DTOs.Users;

public record LoginResponseDto
{
    public string AccessToken { get; set; } = default!;
    public string TokenType { get; set; } = default!;
    public int ExpiresIn { get; set; }
}