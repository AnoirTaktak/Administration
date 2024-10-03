using System.ComponentModel.DataAnnotations;

namespace Administration.Dtos
{
    public class ClientDto
    {
        [Required]
        [StringLength(20)]
        public required string MF_Client { get; set; } // Matricule Fiscale

        [Required]
        [StringLength(100)]
        public required string RS_Client { get; set; } // Raison Sociale

        [Required]
        public required string Adresse_Client { get; set; }

        [Required]
        [StringLength(15)]
        public required string Tel_Client { get; set; }

        [Required]
        public required string Type_Client { get; set; } // Type de client
    }
}
