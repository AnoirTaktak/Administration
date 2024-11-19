using System.ComponentModel.DataAnnotations;

namespace Administration.Models
{
    public enum TypeContrat
    {
        CDD,
        CDI,
        Stage
    }

    public class Employe
    {
        [Key]
        public int ID_Employe { get; set; }

        [Required]
        [StringLength(100)]
        public required string Nom_Employe { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Le format de l'adresse e-mail est invalide.")]
        public required string Email_Employe { get; set; }

        [Required]
        public TypeContrat TypeContrat { get; set; } // Enum pour les types de contrat

        [Required]
        public DateTime DateDebut { get; set; }

        public DateTime? DateFin { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Le salaire doit être un nombre positif.")]
        public decimal Salaire { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression(@"^\d+$", ErrorMessage = "Le CIN doit contenir uniquement des chiffres.")]
        public required string CIN_Employe { get; set; }

        [StringLength(20)]
        [RegularExpression(@"^\d+$", ErrorMessage = "Le CNSS doit contenir uniquement des chiffres.")]
        public string? CNSS_Employe { get; set; } // CNSS nullable

        [Required]
        [StringLength(100)]
        public required string Poste_Employe { get; set; }

        [Phone(ErrorMessage = "Le numéro de téléphone n'est pas valide.")]
        public string? Tel_Employe { get; set; } // Téléphone optionnel mais format validé
    }
}
