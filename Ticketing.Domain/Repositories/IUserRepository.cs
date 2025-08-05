using Ticketing.Domain.Entities;

namespace Ticketing.Domain.Repositories;

public interface IUserRepository
{
    void Create(User user);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<User?> GetByIdAsync(Guid Id, CancellationToken cancellationToken);
    void Delete(User user);
    Task<User?> FindByUserNamePasswordAsync(string email, string hashPassword, CancellationToken cancellationToken);
}