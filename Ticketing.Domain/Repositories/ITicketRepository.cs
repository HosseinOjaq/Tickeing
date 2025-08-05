using Ticketing.Domain.Enums;
using Ticketing.Domain.Entities;

namespace Ticketing.Domain.Repositories;

public interface ITicketRepository
{
    void Create(Ticket ticket);
    Task<List<Ticket>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<List<Ticket>> GetAllAsync(CancellationToken cancellationToken);
    Task<Dictionary<StatusType, int>> GetStatsAsync(CancellationToken cancellationToken);
    Task<Ticket?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    void Delete(Ticket ticket);
}