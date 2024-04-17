using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Examen_certifiant_BLOC3C__Leveil_John.Models;
using Microsoft.AspNetCore.Identity;

namespace Examen_certifiant_BLOC3C__Leveil_John.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Examen_certifiant_BLOC3C__Leveil_John.Models.Offre> Offres { get; set; } = default!;
        public virtual DbSet<Examen_certifiant_BLOC3C__Leveil_John.Models.Panier> Paniers { get; set; } = default!;
        public virtual DbSet<Examen_certifiant_BLOC3C__Leveil_John.Models.Reservation> Reservations { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .Property(u => u.Nom)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<ApplicationUser>()
                .Property(u => u.Prenom)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<ApplicationUser>()
                .Property(u => u.CleCompte)
                .HasMaxLength(100);

            modelBuilder.Entity<Offre>()
                .HasMany(o => o.Paniers)
                .WithMany(p => p.Offres)
                .UsingEntity(j => j.ToTable("RelationOffrePanier"));
        }
    }
}
