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
    public Guid? AssignedToUserId { get; private set; }


    public User? CreatedByUser { get; private set; }
    public User? AssignedToUser { get; private set; }


    public static Ticket Create(string title, string description, Guid CreatedByUserId, PriorityType priority)
        => new()
        {
            Id = Guid.NewGuid(),
            Title = title,
            Description = description,
            Status = StatusType.Open,
            CreatedByUserId = CreatedByUserId,
            CreatedAt = DateTime.UtcNow,
            Priority = priority,
            AssignedToUserId = null,
        };
    public void Update(string title, string description, Guid createdByUserId, Guid assignedToUserId, PriorityType priority, StatusType status)
    {
        Title = title;
        Description = description;
        Status = status;
        Priority = priority;
        CreatedByUserId = createdByUserId;
        AssignedToUserId = assignedToUserId;
    }
    public void Assigne(Guid userId)
    {
        AssignedToUserId = userId;
    }
    public void UpdateStatus(StatusType status)
    {
        Status = status;
    }
}