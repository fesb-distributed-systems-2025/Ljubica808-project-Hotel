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
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("Reservations");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Checkin)
                   .IsRequired();

            builder.Property(r => r.Checkout)
                   .IsRequired();

            builder.Property(r => r.TotalPrice)
                   .HasColumnType("decimal(18,2)");

            // FK prema Room
            builder.HasOne(r => r.Room)
                   .WithMany(rm => rm.Reservations)
                   .HasForeignKey(r => r.RoomId);

            // FK prema Guest
            builder.HasOne(r => r.Guest)
                   .WithMany(g => g.Reservations)
                   .HasForeignKey(r => r.GuestId);
        }
    }
}
