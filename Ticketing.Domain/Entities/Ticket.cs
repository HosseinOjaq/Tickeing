using Ticketing.Domain.Enums;
using Ticketing.Domain.Models;

namespace Ticketing.Domain.Entities;

public class Ticket : BaseEntity<Guid>
{
    public string Title { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public StatusType Status { get; private set; } = default!;
    public PriorityType Priority { get; private set; } = default!;
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public Guid CreatedByUserId { get; private set; }
    public Guid AssignedToUserId { get; private set; }


    public User? CreatedByUser { get; private set; }
    public User? AssignedToUser { get; private set; }
}