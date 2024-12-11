using System.ComponentModel.DataAnnotations;

namespace Administration.Models
{
    public class Fournisseur
    {
        [Key]
        public int ID_Fournisseur { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Le type de fournisseur doit comporter au maximum 50 caractères.")]
        public required string Type_Fournisseur { get; set; } // Utilisation de string pour Type

        [Required(ErrorMessage = "La raison sociale est requise et ne contient pas des chiffres.")]
        [RegularExpression(@"^[A-Za-zÀ-ÿ\s]+$", ErrorMessage = "La raison sociale ne doit contenir que des lettres pas des chiffres.")]
        [StringLength(100, ErrorMessage = "La raison sociale ne doit pas dépasser 100 caractères.")]
        public required string RaisonSociale_Fournisseur { get; set; } // Raison sociale en tant que string

        [Required(ErrorMessage = "Le matricule fiscal est requis et verifié le format : 1234567EXP000.")]
        [RegularExpression(@"^\d{7}[A-Z]{3}\d{3}$", ErrorMessage = "Le matricule fiscal doit être composé de 7 chiffres, suivis de 3 lettres majuscules et de 3 chiffres exemple : 1234567EXP000.")]
        public required string MF_Fournisseur { get; set; }

        [Required(ErrorMessage = "L'adresse e-mail est requise et verifié le format : exemple@gmail.com .")]
        [EmailAddress(ErrorMessage = "Le format de l'email est invalide exemple@gmail.com.")]
        public required string Email_Fournisseur { get; set; }

        [Required(ErrorMessage = "L'adresse est requise.")]
        [StringLength(200, ErrorMessage = "L'adresse ne doit pas dépasser 200 caractères.")]
        public required string Adresse_Fournisseur { get; set; }


        [Required(ErrorMessage = "Le numéro de téléphone est requis et verifié le format : +21612345678 ou 12345678.")]
        [RegularExpression(@"^(\+216)?\d{8}$", ErrorMessage = "Le numéro de téléphone doit être au format +216XXXXXXXX ou XXXXXXXX.")]
        public required string Tel_Fournisseur { get; set; }
    }
}
