using Administration.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using UtilisateurModel = Administration.Models.Utilisateur; // Création de l'alias


namespace Administration.Services.Utilisateur
{
    public interface IUtilisateur_Service
    {
        Task<IEnumerable<UtilisateurModel>> GetAllUtilisateurs();
        Task<UtilisateurModel> GetUtilisateurById(int id);
        Task<string> AddUtilisateur(UtilisateurModel utilisateur);
        Task<string> UpdateUtilisateur(UtilisateurModel utilisateur);
        Task<string> DeleteUtilisateur(UtilisateurModel utilisateur);
        Task<UtilisateurModel> GetUtilisateurByUsername(string username);
        Task<UtilisateurModel> Authenticate(string pseudo, string password);
    }
}
