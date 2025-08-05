using Ticketing.Domain.Enums;

namespace Ticketing.Application.Models.DTOs.Tickets;

public record CreateTicketRequestDto
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public PriorityType Priority { get; set; }
}