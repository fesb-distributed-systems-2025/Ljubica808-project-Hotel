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
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("Rooms");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.RoomNumber)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(r => r.PricePerNight)
                   .HasColumnType("decimal(18,2)");

            builder.Property(r => r.IsAvailable)
                   .IsRequired();

            // 1-N: Room -> Reservations
            builder.HasMany(r => r.Reservations)
                   .WithOne(r => r.Room)
                   .HasForeignKey(r => r.RoomId);
        }
    }
}

