using Administration.Models;

namespace Administration.Services
{
    public interface IFactureVente_Service
    {
        Task<IEnumerable<FactureVente>> GetAllFactures();
        Task<FactureVente> GetFactureByNumero(string numeroFacture); // Récupérer par numéro de facture
        Task<IEnumerable<FactureVente>> GetFacturesByClient(int clientId); // Récupérer par client
        Task<FactureVente> CreateFacture(FactureVenteDto factureVenteDto);
    }
}
