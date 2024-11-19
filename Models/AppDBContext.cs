using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Administration.Models
{
    public class AppDBContext : IdentityDbContext

    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Employe> Employes { get; set; }
        public DbSet<Fournisseur> Fournisseurs { get; set; }
        public DbSet<Societe> Societes { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<FactureVente> FacturesVente { get;set; }
        public DbSet<LigneFacture> LignesFacture { get; set; }
        public DbSet<FactureAchat> FacturesAchat { get; set; }
        public DbSet<Retenue> Retenues { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<TypeDocument> TypesDocuments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Index unique sur MF_Client
            modelBuilder.Entity<Client>()
                .HasIndex(c => c.MF_Client)
                .IsUnique();

            // Index unique sur RS_Client
            modelBuilder.Entity<Client>()
                .HasIndex(c => c.RS_Client)
                .IsUnique();

            modelBuilder.Entity<Client>()
                .Property(c => c.Type_Client)
                .HasConversion<string>();

            // Index sur Nom_Employe (unique)
            modelBuilder.Entity<Employe>()
                .HasIndex(e => e.Nom_Employe)
                .IsUnique();

            // Index sur CIN_Employe (unique)
            modelBuilder.Entity<Employe>()
                .HasIndex(e => e.CIN_Employe)
                .IsUnique();

            // Index sur CNSS_Employe (unique)
            modelBuilder.Entity<Employe>()
                .HasIndex(e => e.CNSS_Employe)
                .IsUnique(false); // CNSS peut être nullable, donc pas nécessairement unique

            modelBuilder.Entity<Fournisseur>()
                .HasIndex(f => f.RaisonSociale_Fournisseur)
                .IsUnique();

            modelBuilder.Entity<Fournisseur>()
                .HasIndex(f => f.MF_Fournisseur)
                .IsUnique();

            modelBuilder.Entity<Fournisseur>()
                .HasIndex(f => f.Email_Fournisseur)
                .IsUnique();

            modelBuilder.Entity<Fournisseur>()
                .HasIndex(f => f.Tel_Fournisseur)
                .IsUnique();

            // Définir l'unicité pour Nom_Utilisateur
            modelBuilder.Entity<Utilisateur>()
                .HasIndex(u => u.Nom_Utilisateur)
                .IsUnique();

            // Définir l'unicité pour Pseudo
            modelBuilder.Entity<Utilisateur>()
                .HasIndex(u => u.Pseudo)
                .IsUnique();

            // Définir l'unicité pour Email_Utilisateur
            modelBuilder.Entity<Utilisateur>()
                .HasIndex(u => u.Email_Utilisateur)
                .IsUnique();
        }

    }
}
