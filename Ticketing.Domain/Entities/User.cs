using Ticketing.Domain.Enums;
using Ticketing.Domain.Models;

namespace Ticketing.Domain.Entities;

public class User : BaseEntity<Guid>
{
    public string FullName { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string Password { get; private set; } = default!;
    public RoleType Role { get; private set; }


    public ICollection<Ticket>? CreatedTickets { get; private set; }
    public ICollection<Ticket>? AssignedTickets { get; private set; }


    public static User Create(string fullName, string email, string password, RoleType role)
       => new()
       {
           Id = Guid.NewGuid(),
           FullName = fullName,
           Email = email,
           Password = password,
           Role = role,
       };

    public void UpdateEmail(string? email = null)
    {
        Email = email;
    }
    public void UpdateFullName(string? fullName = null)
    {
        FullName = fullName;
    }
    public void UpdateRule(RoleType role)
    {
        Role = role;
    }
}