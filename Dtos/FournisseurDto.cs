using System.ComponentModel.DataAnnotations;

namespace Administration.Dtos
{
    public class FournisseurDto
    {
        [Required]
        [StringLength(50, ErrorMessage = "Le type de fournisseur doit comporter au maximum 50 caractères.")]
        public required string Type_Fournisseur { get; set; } // Utilisation de string pour Type

        [Required]
        [StringLength(100, ErrorMessage = "La raison sociale ne doit pas dépasser 100 caractères.")]
        public required string RaisonSociale_Fournisseur { get; set; } // Raison sociale en tant que string

        [Required]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "Le matricule fiscal doit comporter exactement 13 chiffres.")]
        public required string MF_Fournisseur { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Le format de l'adresse e-mail est invalide.")]
        public required string Email_Fournisseur { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "L'adresse ne doit pas dépasser 200 caractères.")]
        public required string Adresse_Fournisseur { get; set; }

        [Required]
        [Phone(ErrorMessage = "Le format du téléphone est invalide.")]
        [RegularExpression(@"^\+?\d{10,15}$", ErrorMessage = "Le numéro de téléphone doit comporter entre 10 et 15 chiffres.")]
        public required string Tel_Fournisseur { get; set; }
    }
}
