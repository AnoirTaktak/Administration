using System.ComponentModel.DataAnnotations;

namespace Administration.Dtos
{
    public class FactureAchatDto
    {

        [Required]
        public required string Numero_FactureAchat { get; set; }

        [Required]
        public DateOnly DateAchat { get; set; } // Date de la facture d'achat

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Montant { get; set; } // Montant de la facture

        [Required]
        public bool EtatPaiement { get; set; } // Etat de paiement : payé ou non payé

        [Required]
        public int ID_Fournisseur { get; set; } // Identifiant du fournisseur associé
        public IFormFile? ImageFacture { get; set; } // Image de la facture 
    }
}
