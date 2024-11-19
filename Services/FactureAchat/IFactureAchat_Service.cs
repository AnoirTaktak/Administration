using FactureAchatModel = Administration.Models.FactureAchat; // Création de l'alias

namespace Administration.Services.FactureAchat
{
    public interface IFactureAchat_Service
    {
        Task<IEnumerable<FactureAchatModel>> GetAllFacturesAchat(); // Récupérer toutes les factures triées par date
        Task<FactureAchatModel> GetFactureAchatById(int id); // Récupérer une facture par ID
        Task<string> AddFactureAchat(FactureAchatModel factureAchat); // Ajouter une facture
        Task<string> UpdateFactureAchat(FactureAchatModel factureAchat); // Modifier une facture
        Task<FactureAchatModel> DeleteFactureAchat(FactureAchatModel factureAchat); // Supprimer une facture
        Task<IEnumerable<FactureAchatModel>> GetFacturesAchatByFournisseur(int fournisseurId); // Récupérer factures par fournisseur
        Task<IEnumerable<FactureAchatModel>> GetFacturesAchatByEtat(bool etatPaiement); // Récupérer factures par état (payé ou non)
    }
}
