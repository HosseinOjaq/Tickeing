namespace Ticketing.Domain.Models;

public abstract class BaseEntity<Tkey>
{
    public Tkey Id { get; set; } = default!;
}