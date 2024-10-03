using Administration.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Administration.Services
{
    public interface IUtilisateur_Service
    {
        Task<IEnumerable<Utilisateur>> GetAllUtilisateurs();
        Task<Utilisateur> GetUtilisateurById(int id);
        Task<Utilisateur> AddUtilisateur(Utilisateur utilisateur);
        Utilisateur UpdateUtilisateur(Utilisateur utilisateur);
        Utilisateur DeleteUtilisateur(Utilisateur utilisateur);
        Task<Utilisateur> GetUtilisateurByUsername(string username);
        Task<Utilisateur> Authenticate(string email, string password);
    }
}
