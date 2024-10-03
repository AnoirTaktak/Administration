using Administration.Models;

namespace Administration.Services
{
    public interface IFournisseur_Service
    {
        Task<IEnumerable<Fournisseur>> GetAllFournisseurs();
        Task<Fournisseur> GetFournisseurById(int id);
        Task<Fournisseur> AddFournisseur(Fournisseur fournisseur);
        Fournisseur UpdateFournisseur(Fournisseur fournisseur);
        Fournisseur DeleteFournisseur(Fournisseur fournisseur);
        Task<Fournisseur> GetFournisseurByRS(string des);
    }
}
