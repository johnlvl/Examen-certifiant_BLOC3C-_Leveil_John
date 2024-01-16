using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Examen_certifiant_BLOC3C__Leveil_John.Models;

namespace Examen_certifiant_BLOC3C__Leveil_John.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Examen_certifiant_BLOC3C__Leveil_John.Models.Offre> Offres { get; set; } = default!;
        public DbSet<Examen_certifiant_BLOC3C__Leveil_John.Models.Panier> Paniers { get; set; } = default!;
        public DbSet<Examen_certifiant_BLOC3C__Leveil_John.Models.Reservation> Reservations { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Offre>()
                .HasMany(o => o.Paniers)
                .WithMany(p => p.Offres)
                .UsingEntity(j => j.ToTable("RelationOffrePanier"));
        }
    }
}
