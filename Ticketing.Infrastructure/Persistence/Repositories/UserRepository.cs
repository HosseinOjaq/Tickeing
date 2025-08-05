using Ticketing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Ticketing.Domain.Repositories;

namespace Ticketing.Infrastructure.Persistence.Repositories;

public class UserRepository(TicketingDbContext context) : IUserRepository
{
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Email.ToLower() == email.ToLower(), cancellationToken);
    }
    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.Users.FindAsync(id);
    }
    public void Create(User user)
    {
        context.Users.Add(user);
    }
    public void Delete(User user)
    {
        context.Users.Remove(user);
    }
    public async Task<User?> FindByUserNamePasswordAsync(string email, string hashPassword, CancellationToken cancellationToken)
    {
        return await context.Users
            .FirstOrDefaultAsync(x => x.Email == email && x.Password == hashPassword, cancellationToken);
    }
}