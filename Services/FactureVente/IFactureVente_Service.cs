using Administration.Models;
using FactureVenteModel = Administration.Models.FactureVente; // Création de l'alias


namespace Administration.Services.FactureVente
{
    public interface IFactureVente_Service
    {
        Task<IEnumerable<FactureVenteModel>> GetAllFactures();
        Task<FactureVenteModel> GetFactureByNumero(string numeroFacture); // Récupérer par numéro de facture
        Task<IEnumerable<FactureVenteModel>> GetFacturesByClient(int clientId); // Récupérer par client
        Task<FactureVenteModel> CreateFacture(FactureVenteDto factureVenteDto);
        Task<FactureVenteModel> UpdateFacture(int facId, FactureVenteModel updatedFacture);
        Task<FactureVenteModel> FindByFacId(int facId);
    }
}
