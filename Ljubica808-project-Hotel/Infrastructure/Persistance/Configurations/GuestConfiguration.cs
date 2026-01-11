using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class GuestConfiguration : IEntityTypeConfiguration<Guest>
    {
        public void Configure(EntityTypeBuilder<Guest> builder)
        {
            builder.ToTable("Guests");

            builder.HasKey(g => g.Id);

            builder.Property(g => g.FirstName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(g => g.LastName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(g => g.DateOfBirth)
                .IsRequired();

            builder.Property(g => g.Email)
                   .HasMaxLength(100);

            builder.Property(g => g.PhoneNumber)
                   .HasMaxLength(30);

            // 1-N: Guest -> Reservations
            builder.HasMany(g => g.Reservations)
                   .WithOne(r => r.Guest)
                   .HasForeignKey(r => r.GuestId);
        }
    }
}

