using Ticketing.Domain.Enums;

namespace Ticketing.Application.Models.DTOs.Tickets;

public class TicketStatsDto
{
    public StatusType Status { get; set; }
    public int Count { get; set; }
}