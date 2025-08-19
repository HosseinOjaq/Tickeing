using Ticketing.Api.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Ticketing.Api.Configurations;
using Microsoft.AspNetCore.Authorization;
using Ticketing.Application.Common.Contracts;
using Ticketing.Application.Models.DTOs.Users;

namespace Ticketing.Api.Controllers;

public class AuthController(IUserService userService) : BaseApiController
{
    [HttpPost("login")]
    public async Task<ApiResult<LoginResponseDto>> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken)
    {
        return await userService.LoginAsync(request, cancellationToken);
    }
    [Authorize]
    [HttpGet("Roles")]
    public ApiResult<List<string>> GetRoles()
    {
       return User.Claims.Where(c => c.Type == ClaimTypes.Role)
                        .Select(c => c.Value)
                        .ToList();
    }
}