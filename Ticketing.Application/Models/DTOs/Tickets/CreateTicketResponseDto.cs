using Ticketing.Domain.Enums;

namespace Ticketing.Application.Models.DTOs.Tickets;

public record CreateTicketResponseDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public StatusType Status { get; set; } = default!;
    public PriorityType Priority { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid CreatedByUserId { get; set; }
    public Guid? AssignedToUserId { get; set; }
}