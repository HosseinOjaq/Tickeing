using Ticketing.Domain.Enums;
using Ticketing.Domain.Entities;
using Ticketing.Common.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Ticketing.Infrastructure;

public class TicketingDbContext(DbContextOptions<TicketingDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Ticket> Tickets { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TicketingDbContext).Assembly);

        modelBuilder.Entity<User>().HasData
            (
               User.Create("hossein ojaq", "hosseinojaq123@gmail.com", SecurityUtility.GetSha256Hash("hossein123"), RoleType.Employee),
               User.Create("admin", "admin@gmail.com", SecurityUtility.GetSha256Hash("admin123"), RoleType.Admin)
            );
    }
}