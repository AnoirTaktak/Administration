using System.ComponentModel.DataAnnotations;

namespace Administration.Models
{
    public class FactureVente
    {
        [Key]
        public int ID_FactureVente { get; set; }

        public required string NumeroFacture { get; set; } // Numéro de la facture

        public DateTime DateFacture { get; set; }

        public decimal Total_FactureVente { get; set; }

        public decimal TimbreFiscale { get; set; }

        // Relations
        [Required]
        public required List<LigneFacture> LignesFacture { get; set; }

        [Required]
        public required Client Client { get; set; }  // Relation avec la classe Client

        // Méthode pour générer le numéro de facture
        public static string GenerateNumeroFacture(AppDBContext context)
        {
            int year = DateTime.Now.Year % 100; // Obtenir les deux derniers chiffres de l'année
            int lastInvoiceNumber = context.FacturesVente
                .Where(f => f.DateFacture.Year == DateTime.Now.Year)
                .Count();

            return $"{year}{lastInvoiceNumber + 1:D3}"; // Format : YY001, YY002, etc.
        }
    }
}
