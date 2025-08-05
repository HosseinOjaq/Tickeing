using Ticketing.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Ticketing.Api.Configurations;
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
}