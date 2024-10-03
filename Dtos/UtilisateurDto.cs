using System.ComponentModel.DataAnnotations;

namespace Administration.Dtos
{
    public class UtilisateurDto
    {
        [Required]
        [StringLength(100)]
        public required string Nom_Utilisateur { get; set; }
        [Required]
        [StringLength(100)]
        public required string Email_Utilisateur { get; set; }

        [Required]
        public required string MotDePasse { get; set; }

        [Required]
        public required string Role { get; set; }
    }
}
