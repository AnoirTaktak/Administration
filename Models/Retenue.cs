using System.ComponentModel.DataAnnotations;

namespace Administration.Models
{
    public class Retenue
    {
        [Key]
        public int ID_Retenue { get; set; } // Identifiant unique de la retenue

        [Required]
        [Range(0, 100)]
        public decimal Taux { get; set; } // Taux de la retenue (en pourcentage)

        [Required]
        public required string Categorie { get; set; } // Catégorie de la retenue

        [Required]
        public int ID_FactureAchat { get; set; } // Identifiant de la facture d'achat associée

        // Navigation property
        public FactureAchat? FactureAchat { get; set; } // Relation avec la facture d'achat
    }
}
