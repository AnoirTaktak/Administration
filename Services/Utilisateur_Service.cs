using Administration.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Administration.Services
{
    public class Utilisateur_Service : IUtilisateur_Service
    {
        private readonly AppDBContext _context;

        public Utilisateur_Service(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Utilisateur>> GetAllUtilisateurs()
        {
            return await _context.Utilisateurs.ToListAsync();
        }

        public async Task<Utilisateur> GetUtilisateurById(int id)
        {
            return await _context.Utilisateurs.SingleOrDefaultAsync(u => u.ID_Utilisateur == id);
        }

        public async Task<Utilisateur> AddUtilisateur(Utilisateur utilisateur)
        {
            await _context.AddAsync(utilisateur);
            _context.SaveChanges();
            return utilisateur;
        }

        public Utilisateur UpdateUtilisateur(Utilisateur utilisateur)
        {
            _context.Update(utilisateur);
            _context.SaveChanges();
            return utilisateur;
        }

        public Utilisateur DeleteUtilisateur(Utilisateur utilisateur)
        {
            _context.Remove(utilisateur);
            _context.SaveChanges();
            return utilisateur;
        }

        public async Task<Utilisateur> GetUtilisateurByUsername(string username)
        {
            return await _context.Utilisateurs.SingleOrDefaultAsync(u => u.Nom_Utilisateur == username);
        }

        public async Task<Utilisateur> Authenticate(string email, string password)
        {
            // Recherche l'utilisateur avec l'email donné
            var utilisateur = await _context.Utilisateurs.SingleOrDefaultAsync(u => u.Email_Utilisateur == email);

            // Vérifie si l'utilisateur existe et si le mot de passe est correct (à sécuriser avec le hashing)
            if (utilisateur == null || utilisateur.MotDePasse_Utilisateur != password)
            {
                return null;
            }

            return utilisateur;
        }
    }
}
