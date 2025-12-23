using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class HotelDbContext : DbContext, IApplicationDbContext
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HotelDbContext).Assembly);
        }
    }
}
/*
IApplicationDbContext: služi za apstrakciju nad DbContext-om i omogućuje lakše testiranje i razdvajanje slojeva aplikacije.
VEza između C# klasa i baze podataka koristeći entity framework(Bez korištenja SQL)

 */