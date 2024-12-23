using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
namespace Administration.Models
{
    public class LigneFacture
    {
        [Key]
        public int ID_LigneFV { get; set; }
        public int ID_Service { get; set; }
        [AllowNull]
        public int? ID_FactureVente { get; set; }
        public decimal Quantite { get; set; }
        public decimal Total_LigneFV { get; set; }
        public decimal Total_LigneHT { get; set; }
    }
}