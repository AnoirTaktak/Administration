using System.ComponentModel.DataAnnotations;

namespace Administration.Models
{
    public class TypeDocument
    {
        [Key]
        public int ID_TypeDocument { get; set; } // Identifiant unique du type de document
        [Required]
        public required string NomType { get; set; } // Nom du type (ex: Attestation de Travail)
        public required string Template { get; set; } // Template pour le document
    }
}
