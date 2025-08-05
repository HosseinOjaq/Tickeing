using Ticketing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ticketing.Infrastructure.Persistence.Configuration;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder
           .Property(x => x.Title)
           .HasMaxLength(30)
           .IsRequired();

        builder
            .Property(x => x.Description)
            .HasMaxLength(300)
            .IsRequired();

        builder
            .HasOne(x => x.CreatedByUser)
            .WithMany(x => x.CreatedTickets)
            .HasForeignKey(x => x.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(x => x.AssignedToUser)
            .WithMany(x => x.AssignedTickets)
            .HasForeignKey(x => x.AssignedToUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}