using System.ComponentModel.DataAnnotations;

namespace Administration.Dtos
{
    public class DocumentDto
    {
        [Required]
        public int ID_Employe { get; set; } // Identifiant de l'employé
        [Required]
        public int ID_TypeDocument { get; set; } // Identifiant du type de document
        [Required]
        public DateTime Date { get; set; } // Date de création du document
        public required string Contenu { get; set; } // Contenu du document
    }
}
