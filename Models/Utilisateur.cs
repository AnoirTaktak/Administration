using System.ComponentModel.DataAnnotations;

namespace Administration.Models
{
    public enum Role
    {
        SuperAdmin,
        Admin,
        User
    }

    public class Utilisateur
    {
        [Key]
        public int ID_Utilisateur { get; set; }

        [Required]
        [StringLength(100)]
        public required string Nom_Utilisateur { get; set; }

        public required string Pseudo { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Le format de l'adresse e-mail est invalide.")]
        public required string Email_Utilisateur { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$",
            ErrorMessage = "Le mot de passe doit contenir au moins 6 caractères, dont une majuscule, un chiffre et un caractère spécial.")]
        public required string MotDePasse_Utilisateur { get; set; }

        [Required]
        public Role Role_Utilisateur { get; set; }
    }
}