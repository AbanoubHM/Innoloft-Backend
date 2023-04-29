using Microsoft.EntityFrameworkCore;

namespace Innoloft_Backend.Models {
    public class EventsContext : DbContext {
        public EventsContext() {

        }

        public EventsContext(DbContextOptions<EventsContext> options) : base(options){
            
        }

        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Event>(entity => {
                entity.HasOne(d => d.User).WithMany(d => d.Events).HasForeignKey(d => d.UserId);
                entity.HasMany(d => d.ParticipatingUsers).WithMany(d => d.ParticipatingEvents).UsingEntity<Dictionary<int,int>>("ParticipatingEventUser");
                entity.HasMany(d => d.InvitedUsers).WithMany(d => d.InvitedEvents).UsingEntity<Dictionary<int, int>>("InvitedEventUser");
            });
            modelBuilder.Entity<User>().OwnsOne(u => u.Address);
            modelBuilder.Entity<Event>().OwnsOne(u => u.Address);
        }


    }
}
