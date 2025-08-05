using Ticketing.Domain.Enums;
using Ticketing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Ticketing.Domain.Repositories;

namespace Ticketing.Infrastructure.Persistence.Repositories;

public class TicketRepository(TicketingDbContext context) : ITicketRepository
{
    public void Create(Ticket ticket)
    {
        context.Tickets.Add(ticket);
    }
    public void Delete(Ticket ticket)
    {
        context.Tickets.Remove(ticket);
    }
    public async Task<List<Ticket>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await context.Tickets
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
    public async Task<Ticket?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.Tickets
            .SingleOrDefaultAsync(t => t.Id == id, cancellationToken);
    }
    public async Task<List<Ticket>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await context.Tickets
            .AsNoTracking()
            .Where(t => t.CreatedByUserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<Dictionary<StatusType, int>> GetStatsAsync(CancellationToken cancellationToken)
    {
        return await context.Tickets
            .GroupBy(t => t.Status)
            .Select(g => new { Status = g.Key, Count = g.Count() })
            .ToDictionaryAsync(x => x.Status, x => x.Count, cancellationToken);
    }
}