﻿namespace Ticketing.Application.Models.DTOs.Users;

public record LoginRequestDto
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}