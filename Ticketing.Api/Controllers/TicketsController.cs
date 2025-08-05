using Ticketing.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Ticketing.Api.Extensions;
using Ticketing.Api.Configurations;
using Microsoft.AspNetCore.Authorization;
using Ticketing.Application.Common.Contracts;
using Ticketing.Application.Models.DTOs.Tickets;

namespace Ticketing.Api.Controllers;

[Authorize]
public class TicketsController(ITicketService ticketService, IHttpContextAccessor httpContextAccessor) : BaseApiController
{
    [HttpPost]
    [Authorize(Roles = "Employee")]
    public async Task<ApiResult<CreateTicketResponseDto>> CreateAsync( CreateTicketRequestDto request, CancellationToken cancellationToken)
    {
        var userId = httpContextAccessor.GetUserId();
        return await ticketService.CreateAsync(userId, request, cancellationToken);
    }

    [HttpGet("my")]
    [Authorize(Roles = "Employee")]
    public async Task<ApiResult<List<TicketDto>>> MyTicketsAsync(CancellationToken cancellationToken)
    {
        var userId = httpContextAccessor.GetUserId();
        return await ticketService.GetMyTicketsAsync(userId, cancellationToken);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResult<List<TicketDto>>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await ticketService.GetAllAsync(cancellationToken);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResult<TicketDto>> AssignAsync(Guid id, CancellationToken cancellationToken)
    {
        var userId = httpContextAccessor.GetUserId();
        return await ticketService.AssignmentAsync(id, userId, cancellationToken);
    }

    [HttpGet("stats")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResult<List<TicketStatsDto>>> GetStatsAsync(CancellationToken cancellationToken)
    {
        return await ticketService.GetTicketStatsAsync(cancellationToken);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResult<TicketDto?>> GetBayIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var userId = httpContextAccessor.GetUserId();
        return await ticketService.GetByIdAsync(id, userId, cancellationToken);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await ticketService.DeleteAsync(id, cancellationToken);
        return Ok();
    }
}