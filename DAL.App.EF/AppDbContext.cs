using System.Linq;
using Microsoft.EntityFrameworkCore;
using Reservation = Domain.App.Reservation;

namespace DAL.App.EF
{
    public class AppDbContext :  DbContext
    {

        public DbSet<Reservation> Reservations { get; set; } = default!;

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {


            base.OnModelCreating(builder);

            // disable cascade delete initially for everything
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

        }

    }
}