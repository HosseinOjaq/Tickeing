using Ticketing.Domain.Entities;

namespace Ticketing.Domain.Repositories;

public interface IUserRepository
{
    void Create(User user);
    void Delete(User user);
    Task<User?> GetByIdAsync(Guid Id, CancellationToken cancellationToken);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<User?> FindByUserNamePasswordAsync(string email, string hashPassword, CancellationToken cancellationToken);
}