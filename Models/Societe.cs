using System.ComponentModel.DataAnnotations;

namespace Administration.Models
{
    public class Societe
    {
        [Key]
        public int ID_Societe { get; set; }

        [Required]
        [StringLength(20)]
        public required string MF_Societe { get; set; } // Matricule Fiscale

        [Required]
        [StringLength(100)]
        public required string RaisonSociale_Societe { get; set; } // Raison Sociale

        [Required]
        [StringLength(200)]
        public required string Adresse_Societe { get; set; } // Adresse

        [Required]
        [StringLength(15)]
        public required string Tel_Societe { get; set; } // Téléphone

        [Required]
        [StringLength(10)]
        public required string CodePostal { get; set; } // Code Postal

        public required byte[] Cachet { get; set; } // Cachet (stocké sous forme de tableau d'octets)

        public required byte[] Signature { get; set; } // Signature (stockée sous forme de tableau d'octets)
    }
}
