using System.ComponentModel.DataAnnotations;

namespace Administration.Models
{
    public class Document
    {
        [Key]
        public int ID_Document { get; set; } // Identifiant unique du document
        [Required]
        public int ID_Employe { get; set; } // Identifiant de l'employé
        [Required]
        public int ID_TypeDocument { get; set; } // Identifiant du type de document
        [Required]
        public DateTime Date { get; set; } // Date de création du document
        public required string Contenu { get; set; } // Contenu complet du document
    }
}
