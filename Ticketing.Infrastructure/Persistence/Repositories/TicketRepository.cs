using Ticketing.Domain.Repositories;

namespace Ticketing.Infrastructure.Persistence.Repositories;

public class TicketRepository(TicketingDbContext context) : ITicketRepository
{

}