using System.ComponentModel.DataAnnotations;

namespace Administration.Models
{
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
        public required string TypeContrat { get; set; }
        [Required]
        public DateTime DateDebut { get; set; }
        public DateTime? DateFin { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Le salaire doit être un nombre positif.")]
        public decimal Salaire { get; set; }
        [Required]
        [StringLength(20)]
        public required string CIN_Employe { get; set; }
        [Required]
        [StringLength(20)]
        public required string CNSS_Employe { get; set; }
        [Required]
        [StringLength(100)]
        public required string Poste_Employe { get; set; }


    }
}
