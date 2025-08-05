namespace Ticketing.Application.Models.DTOs.Users;

public record UpdateUserRequestDto
{
    public Guid userId { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
}