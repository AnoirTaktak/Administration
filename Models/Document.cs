using System.ComponentModel.DataAnnotations;

namespace Administration.Models
{
    public class Document
    {
        [Key]
        public int ID_Document { get; set; } // Identifiant unique

        [Required]
        public int ID_Employe { get; set; } // Employé concerné

        [Required]
        public int ID_TypeDocument { get; set; } // Type de document

        [Required]
        public int ID_Societe { get; set; } // les donnés de societe

        [Required]
        public DateTime Date { get; set; } // Date de création

        public required string Contenu { get; set; } // Contenu final

        // Nouvelle colonne pour stocker le fichier PDF
        public byte[]? Doc_Pdf { get; set; }
    }
}
