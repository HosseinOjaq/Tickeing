using Ticketing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ticketing.Infrastructure.Persistence.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
           .Property(x => x.FullName)
           .HasMaxLength(60)
           .IsRequired();

        builder
           .Property(x => x.Email)
           .HasMaxLength(70)
           .IsRequired();

        builder
           .Property(x => x.Password)
           .HasMaxLength(60)
           .IsRequired();

        builder
            .HasMany(x => x.Tickets)
            .WithOne(x => x.CreatedByUser)
            .HasForeignKey(x => x.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(x => x.Tickets)
            .WithOne(x => x.AssignedToUser)
            .HasForeignKey(x => x.AssignedToUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}