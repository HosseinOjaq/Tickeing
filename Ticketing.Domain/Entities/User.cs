using Ticketing.Domain.Enums;
using Ticketing.Domain.Models;

namespace Ticketing.Domain.Entities;

public class User : BaseEntity<Guid>
{
    public string FullName { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string Password { get; private set; } = default!;
    public RoleType Role { get; private set; }


    public ICollection<Ticket>? Tickets { get; private set; }
}