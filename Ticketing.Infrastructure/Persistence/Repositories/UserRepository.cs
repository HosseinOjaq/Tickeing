using Ticketing.Domain.Repositories;

namespace Ticketing.Infrastructure.Persistence.Repositories;

public class UserRepository(TicketingDbContext context) : IUserRepository
{

}