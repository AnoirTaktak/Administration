using Administration.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Administration.Models
{
    public class FactureVenteDto
    {
        // Retirer la propriété NumeroFacture
        // public required string NumeroFacture { get; set; }  // Numéro de la facture (à retirer)

        public DateTime DateFacture { get; set; }  // Date de la facture
        public decimal Total_FactureVente { get; set; }  // Total de la facture
        public decimal TimbreFiscale { get; set; }  // Timbre fiscal
        public required ClientDto Client { get; set; }  // Informations sur le client
        public required List<LigneFactureDto> LignesFacture { get; set; }  // Lignes de facture
    }
}
