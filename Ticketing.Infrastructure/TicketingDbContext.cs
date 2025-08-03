using Ticketing.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ticketing.Infrastructure;

public class TicketingDbContext(DbContextOptions<TicketingDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Ticket> Tickets => Set<Ticket>();
}