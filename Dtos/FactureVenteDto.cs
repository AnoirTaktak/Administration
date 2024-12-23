using Administration.Dtos;


namespace Administration.Models
{
    public class FactureVenteDto
    {
        
        public DateTime DateFacture { get; set; }  // Date de la facture
        public decimal Total_FactureVente { get; set; }  // Total de la facture
        public decimal TimbreFiscale { get; set; }  // Timbre fiscal
        public decimal? Total_FactureVenteHT { get; set; }
        public decimal? Remise { get; set; }
        public required ClientDto Client { get; set; }  // Informations sur le client
        public required List<LigneFactureDto> LignesFacture { get; set; }  // Lignes de facture
    }
}
