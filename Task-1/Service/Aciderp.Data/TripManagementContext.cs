using Aciderp.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Aciderp.Data
{
    public class TripManagementContext : DbContext
    {
		public TripManagementContext (DbContextOptions<TripManagementContext> options)
            : base(options) { }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Trip> Trips { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>().ToTable("Address");
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Trip>().ToTable("Trip");
        }
    }
}
