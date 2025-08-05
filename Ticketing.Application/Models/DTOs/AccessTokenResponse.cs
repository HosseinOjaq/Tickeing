using System.IdentityModel.Tokens.Jwt;

namespace Ticketing.Application.Models.DTOs;

public record AccessTokenResponse(JwtSecurityToken SecurityToken)
{
    public string AccessToken { get; set; } = new JwtSecurityTokenHandler().WriteToken(SecurityToken);    
    public string TokenType { get; set; } = "Bearer";
    public int ExpiresIn { get; set; } = (int)(SecurityToken.ValidTo - DateTime.UtcNow).TotalSeconds;
}