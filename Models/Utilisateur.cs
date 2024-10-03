using System.ComponentModel.DataAnnotations;

namespace Administration.Models
{
    public class Utilisateur
    {
        [Key]
        public int ID_Utilisateur { get; set; }

        [Required]
        [StringLength(100)]
        public required string Nom_Utilisateur { get; set; }

        [Required]
        [EmailAddress]
        public required string Email_Utilisateur { get; set; }

        [Required]
        public required string MotDePasse_Utilisateur { get; set; }

        [Required]
        public required string Role_Utilisateur { get; set; } 
    }
}
