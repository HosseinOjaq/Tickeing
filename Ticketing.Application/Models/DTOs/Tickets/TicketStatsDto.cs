using System.Security.Cryptography;
using Ticketing.Domain.Enums;

namespace Ticketing.Application.Models.DTOs.Tickets;

public class TicketStatsDto
{
    public string Status { get; set; }
    public int Count { get; set; }
}