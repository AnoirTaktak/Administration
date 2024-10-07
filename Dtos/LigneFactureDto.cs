using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Administration.Dtos
{
    public class LigneFactureDto
    {
        [Required]
        public int ID_Service { get; set; }

        [AllowNull]
        public int? ID_FactureVente { get; set; }

        [Required]
        public int Quantite { get; set; }

        [Required]
        public decimal Total_LigneFV { get; set; }
    }
}
