using Administration.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UtilisateurModel = Administration.Models.Utilisateur; // Création de l'alias


namespace Administration.Services.Utilisateur
{
    public class Utilisateur_Service : IUtilisateur_Service
    {
        private readonly AppDBContext _context;

        public Utilisateur_Service(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UtilisateurModel>> GetAllUtilisateurs()
        {
            return await _context.Utilisateurs.ToListAsync();
        }

        public async Task<UtilisateurModel> GetUtilisateurById(int id)
        {
            var user = await _context.Utilisateurs.SingleOrDefaultAsync(u => u.ID_Utilisateur == id);
            if (user == null)
            {
                throw new ArgumentException($"Utilisateur avec ID : '{id}' n'existe pas.");
            }
            return user;
        }

        public async Task<string> AddUtilisateur(UtilisateurModel utilisateur)
        {
            // Vérifier si le pseudo, le nom d'utilisateur ou l'email existent déjà
            if (await _context.Utilisateurs.AnyAsync(u => u.Pseudo == utilisateur.Pseudo ||
                                                         u.Nom_Utilisateur == utilisateur.Nom_Utilisateur ||
                                                         u.Email_Utilisateur == utilisateur.Email_Utilisateur))
            {
                return "Erreur: Le Nom , Pseudou ou Email est déja utilisé .";
            }

            // Hachage du mot de passe avant d'enregistrer l'utilisateur
            utilisateur.MotDePasse_Utilisateur = BCrypt.Net.BCrypt.HashPassword(utilisateur.MotDePasse_Utilisateur);

            await _context.AddAsync(utilisateur);
            await _context.SaveChangesAsync();
            return "Utilisateur ajouté avec succès.";
        }

        public async Task<string> UpdateUtilisateur(UtilisateurModel utilisateur)
        {
            // Vérifier si le pseudo, le nom d'utilisateur ou l'email existent déjà
            if (await _context.Utilisateurs.AnyAsync(u => (u.Pseudo == utilisateur.Pseudo ||
                                                         u.Nom_Utilisateur == utilisateur.Nom_Utilisateur ||
                                                         u.Email_Utilisateur == utilisateur.Email_Utilisateur) && u.ID_Utilisateur != utilisateur.ID_Utilisateur ))
            {
                return "Erreur: Le Nom , Pseudou ou Email est déja utilisé .";
            }

            utilisateur.MotDePasse_Utilisateur = BCrypt.Net.BCrypt.HashPassword(utilisateur.MotDePasse_Utilisateur);

            _context.Update(utilisateur);
            _context.SaveChanges();
            return "Utilisateur modifié avec succès.";
        }

        public UtilisateurModel DeleteUtilisateur(UtilisateurModel utilisateur)
        {
            _context.Remove(utilisateur);
            _context.SaveChanges();
            return utilisateur;
        }

        public async Task<UtilisateurModel> GetUtilisateurByUsername(string username)
        {
            var user = await _context.Utilisateurs.SingleOrDefaultAsync(u => u.Nom_Utilisateur == username);
            if (user == null)
            {
                throw new ArgumentException($"Utilisateur avec ce Nom : '{username}' n'existe pas.");
            }
            return user;
        }

        public async Task<UtilisateurModel> Authenticate(string pseudo, string password)
        {
            // Recherche l'utilisateur avec l'email donné
            var utilisateur = await _context.Utilisateurs.SingleOrDefaultAsync(u => u.Pseudo == pseudo);

            // Vérifie si l'utilisateur existe et si le mot de passe est correct
            if (utilisateur == null || !BCrypt.Net.BCrypt.Verify(password, utilisateur.MotDePasse_Utilisateur))
            {
                throw new ArgumentException($"Pseudo ou Mot de passe Incorrecte :'{pseudo}'.");
            }

            return utilisateur;
        }




    }
}
