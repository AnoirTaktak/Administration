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
    }
}
