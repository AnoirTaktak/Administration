using Administration.Models;
using FournisseurModel = Administration.Models.Fournisseur; // Création de l'alias


namespace Administration.Services.Fournisseur
{
    public interface IFournisseur_Service
    {
        Task<IEnumerable<FournisseurModel>> GetAllFournisseurs();
        Task<FournisseurModel> GetFournisseurById(int id);
        Task<string> AddFournisseur(FournisseurModel fournisseur);
        string UpdateFournisseur(FournisseurModel fournisseur);
        FournisseurModel DeleteFournisseur(FournisseurModel fournisseur);
        Task<IEnumerable<FournisseurModel>> GetFournisseurByRS(string rs);
        Task<IEnumerable<FournisseurModel>> GetFournisseurByMF(string mf);
    }
}
