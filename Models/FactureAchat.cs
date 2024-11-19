using System.ComponentModel.DataAnnotations;

namespace Administration.Models
{
    public class FactureAchat
    {
        [Key]
        public int ID_FactureAchat { get; set; } // Identifiant unique de la facture d'achat

        [Required]
        public DateTime DateAchat { get; set; } // Date de la facture d'achat

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Montant { get; set; } // Montant de la facture

        [Required]
        public bool EtatPaiement { get; set; } // Etat de paiement : payé ou non payé
        [Required]
        public int ID_Fournisseur { get; set; } // Identifiant du fournisseur associé
        public byte[]? ImageFacture { get; set; } // Image de la facture sous forme de tableau d'octets
    }
}
