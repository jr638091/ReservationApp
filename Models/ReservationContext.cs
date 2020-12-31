using Microsoft.EntityFrameworkCore;

namespace ReservationApp.Models
{
    public class ReservationContext: DbContext {
        public ReservationContext (DbContextOptions<ReservationContext> options): base(options){}
        public DbSet<ContactType> ContactTypes { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}