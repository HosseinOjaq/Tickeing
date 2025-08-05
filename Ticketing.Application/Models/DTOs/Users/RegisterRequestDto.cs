using Ticketing.Domain.Enums;

namespace Ticketing.Application.Models.DTOs.Users;

public class RegisterRequestDto
{
    public string FullName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public RoleType Role { get; set; }
}