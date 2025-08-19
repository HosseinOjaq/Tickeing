using Ticketing.Domain.Enums;
using Ticketing.Common.Models;
using Ticketing.Domain.Entities;
using Ticketing.Domain.Repositories;
using Ticketing.Application.Common.Contracts;
using Ticketing.Application.Models.DTOs.Tickets;
using Ticketing.Common.Extensions;

namespace Ticketing.Application.Services;

public class TicketService(IUnitOfWork uow, ITicketRepository ticketRepository) : ITicketService
{
    public async Task<OperationResult<CreateTicketResponseDto>> CreateAsync(Guid CreatedByUserId, CreateTicketRequestDto request, CancellationToken cancellationToken)
    {
        var ticket = Ticket.Create(request.Description, request.Title, CreatedByUserId, request.Priority);
        ticketRepository.Create(ticket);
        var saveChangesResult = await uow.SaveChangesAsync(cancellationToken);
        if (!saveChangesResult.IsSuccess)
            return OperationResult<CreateTicketResponseDto>.Fail();

        return new CreateTicketResponseDto
        {
            Id = ticket.Id,
            Title = ticket.Title,
            Status = ticket.Status,
            Priority = ticket.Priority,
            CreatedAt = ticket.CreatedAt,
            UpdatedAt = ticket.UpdatedAt,
            Description = ticket.Description,
            CreatedByUserId = ticket.CreatedByUserId,
            AssignedToUserId = ticket.AssignedToUserId,
        };
    }

    public async Task<OperationResult> DeleteAsync(Guid ticketId, CancellationToken cancellationToken)
    {
        var ticket = await ticketRepository.GetByIdAsync(ticketId, cancellationToken);
        if (ticket is null)
            return ErrorModel.Create("InvalidId", "شناسه نامعتبر");

        ticketRepository.Delete(ticket);
        var saveChangesResult = await uow.SaveChangesAsync(cancellationToken);
        if (!saveChangesResult.IsSuccess)
            return OperationResult.Fail();
        return OperationResult.Success();
    }

    public async Task<OperationResult<List<TicketDto>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var tickets = await ticketRepository.GetAllAsync(cancellationToken);
        var result = tickets.Select(x => new TicketDto
        {
            Id = x.Id,
            Status = x.Status,
            Title = x.Title,
            Priority = x.Priority,
            CreatedAt = x.CreatedAt,
            Description = x.Description,
            CreatedByUserId = x.CreatedByUserId,
            AssignedToUserId = x.AssignedToUserId,
        }).ToList();

        return result;
    }

    public async Task<OperationResult<List<TicketDto>>> GetMyTicketsAsync(Guid userId, CancellationToken cancellationToken)
    {
        var tickets = await ticketRepository.GetByUserIdAsync(userId, cancellationToken);
        var result = tickets.Select(x => new TicketDto
        {
            Id = x.Id,
            Status = x.Status,
            Title = x.Title,
            Priority = x.Priority,
            CreatedAt = x.CreatedAt,
            Description = x.Description,
            CreatedByUserId = x.CreatedByUserId,
            AssignedToUserId = x.AssignedToUserId,
        }).ToList();

        return result;
    }

    public async Task<OperationResult<TicketDto?>> GetByIdAsync(Guid ticketId, Guid requesterUserId, CancellationToken cancellationToken)
    {
        var ticket = await ticketRepository.GetByIdAsync(ticketId, cancellationToken);
        if (ticket is null)
            return ErrorModel.Create("InvalidId", "شناسه نامعتبر");

        if (ticket.CreatedByUserId != requesterUserId && ticket.AssignedToUserId != requesterUserId)
            return ErrorModel.Create("InvalidId", "شما اجازه دسترسی به این تیکت را ندارید.");

        var result = new TicketDto
        {
            Id = ticket.Id,
            Status = ticket.Status,
            Title = ticket.Title,
            Priority = ticket.Priority,
            CreatedAt = ticket.CreatedAt,
            Description = ticket.Description,
            CreatedByUserId = ticket.CreatedByUserId,
            AssignedToUserId = ticket.AssignedToUserId
        };

        return result;
    }

    public async Task<OperationResult<List<TicketStatsDto>>> GetTicketStatsAsync(CancellationToken cancellationToken)
    {
        var stats = await ticketRepository.GetStatsAsync(cancellationToken);
        var result = stats.Select(x => new TicketStatsDto
        {
            Status = x.Key.ToDisplay().ToString(),
            Count = x.Value
        })
        .ToList();

        return result;
    }

    public async Task<OperationResult<UpdateTicketResponseDto>> UpdateAsync(UpdateTicketRequestDto request,Guid userId, CancellationToken cancellationToken)
    {
        var ticket = await ticketRepository.GetByIdAsync(request.Id, cancellationToken);
        if (ticket is null)
            return ErrorModel.Create("InvalidId", "شناسه نامعتبر");

        if (ticket.CreatedByUserId !=userId)
            return ErrorModel.Create("InvalidId", "کاربری که تیکت را ایجاد کرده اجازه دارد");

        ticket.Update(request.Title,
            request.Description,
            request.CreatedByUserId,
            request.AssignedToUserId,
            request.Priority,
            request.Status);

        var saveChangesResult = await uow.SaveChangesAsync(cancellationToken);
        if (!saveChangesResult.IsSuccess)
            return OperationResult<UpdateTicketResponseDto>.Fail();

        return new UpdateTicketResponseDto
        {
            Id = ticket.Id,
            Title = ticket.Title,
            Status = ticket.Status,
            Priority = ticket.Priority,
            CreatedAt = ticket.CreatedAt,
            UpdatedAt = ticket.UpdatedAt,
            Description = ticket.Description,
            CreatedByUserId = ticket.CreatedByUserId,
            AssignedToUserId = ticket.CreatedByUserId
        };
    }

    public async Task<OperationResult<TicketDto>> AssignmentAsync(Guid ticketId, Guid userId, CancellationToken cancellationToken)
    {
        var ticket = await ticketRepository.GetByIdAsync(ticketId, cancellationToken);
        if (ticket is null)
            return ErrorModel.Create("InvalidId", "شناسه نامعتبر");

        ticket.Assigne(userId);
        ticket.UpdateStatus(StatusType.InProgress);

        var saveChangesResult = await uow.SaveChangesAsync(cancellationToken);
        if (!saveChangesResult.IsSuccess)
            return OperationResult<TicketDto>.Fail();

        return new TicketDto
        {
            Id = ticket.Id,
            Title = ticket.Title,
            Status = ticket.Status,
            Priority = ticket.Priority,
            CreatedAt = ticket.CreatedAt,
            Description = ticket.Description,
            CreatedByUserId = ticket.CreatedByUserId,
            AssignedToUserId = ticket.AssignedToUserId
        };
    }
}