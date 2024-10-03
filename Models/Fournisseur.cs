using System.ComponentModel.DataAnnotations;

namespace Administration.Models
{
    public class Fournisseur
    {
        [Key]
        public int ID_Fournisseur { get; set; }
        [Required]
        public required string Type_Fournisseur { get; set; }
        [Required]
        [StringLength(100)]
        public required string RaisonSociale_Fournisseur { get; set; }
        [Required]
        [StringLength(20)]
        public int MF_Fournisseur { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Le format de l'adresse e-mail est invalide.")]
        public required string Email_Fournisseur { get; set; }
        [Required]
        public required string Adresse_Fournisseur { get; set; }
        [Required]
        [Phone]
        public required string Tel_Fournisseur { get; set; }
    }
}
