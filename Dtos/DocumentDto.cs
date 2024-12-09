using System.ComponentModel.DataAnnotations;

namespace Administration.Dtos
{
    public class DocumentDto
    {
        [Required]
        public int ID_Employe { get; set; }

        [Required]
        public int ID_TypeDocument { get; set; }

        [Required]
        public int ID_Societe { get; set; }

        public DateTime? Date { get; set; }
        public string? Contenu { get; set; } // Contenu final

        // Nouvelle propriété pour le fichier PDF
        public IFormFile? Doc_Pdf { get; set; }
    }
}
