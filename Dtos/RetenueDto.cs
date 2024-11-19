using System.ComponentModel.DataAnnotations;

namespace Administration.Dtos
{
    public class RetenueDto
    {
        [Required]
        public int ID_Retenue { get; set; } // Identifiant unique de la retenue

        [Required]
        [Range(0, 100)]
        public decimal Taux { get; set; } // Taux de la retenue (en pourcentage)

        [Required]
        public required string Categorie { get; set; } // Catégorie de la retenue

        [Required]
        public int ID_FactureAchat { get; set; } // Identifiant de la facture d'achat associée
    }
}
