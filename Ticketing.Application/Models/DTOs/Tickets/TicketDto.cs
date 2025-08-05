using Ticketing.Domain.Enums;

namespace Ticketing.Application.Models.DTOs.Tickets;

public record TicketDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public StatusType Status { get; set; }
    public PriorityType Priority { get; set; }
    public Guid CreatedByUserId { get; set; }
    public Guid? AssignedToUserId { get; set; }
    public DateTime CreatedAt { get; set; }
}