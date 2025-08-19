using Ticketing.Common.Models;
using Ticketing.Application.Models.DTOs.Tickets;

namespace Ticketing.Application.Common.Contracts;

public interface ITicketService
{
    Task<OperationResult> DeleteAsync(Guid ticketId, CancellationToken cancellationToken);
    Task<OperationResult<List<TicketDto>>> GetAllAsync(CancellationToken cancellationToken);
    Task<OperationResult<List<TicketStatsDto>>> GetTicketStatsAsync(CancellationToken cancellationToken);
    Task<OperationResult<List<TicketDto>>> GetMyTicketsAsync(Guid userId, CancellationToken cancellationToken);
    Task<OperationResult<TicketDto>> AssignmentAsync(Guid ticketId, Guid userId, CancellationToken cancellationToken);
    Task<OperationResult<TicketDto?>> GetByIdAsync(Guid ticketId, Guid requesterUserId, CancellationToken cancellationToken);
    Task<OperationResult<UpdateTicketResponseDto>> UpdateAsync(UpdateTicketRequestDto request, Guid userId, CancellationToken cancellationToken);
    Task<OperationResult<CreateTicketResponseDto>> CreateAsync(Guid CreatedByUserId,CreateTicketRequestDto request, CancellationToken cancellationToken);
}