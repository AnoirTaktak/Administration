using Administration.Models;
using Microsoft.EntityFrameworkCore;
using SocieteModel = Administration.Models.Societe; // Création de l'alias

namespace Administration.Services.Societe
{
    public class Societe_Service : ISociete_Service
    {
        private readonly AppDBContext _context;

        public Societe_Service(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SocieteModel>> GetAllSocietes()
        {
            return await _context.Societes.ToListAsync();
        }

        public async Task<SocieteModel> GetSocieteById(int id)
        {
            var societe = await _context.Societes.SingleOrDefaultAsync(s => s.ID_Societe == id);
            if (societe == null)
            {
                return null;
            }

            return societe;
        }

        public async Task<string> AddSociete(SocieteModel societe)
        {
            // Vérification si la société existe déjà (selon le Matricule Fiscale ou la Raison Sociale par exemple)
            if (_context.Societes.Any(s => s.MF_Societe == societe.MF_Societe))
            {
                return $"Erreur: Le Matricule Fiscal '{societe.MF_Societe}' est déjà utilisé.";
            }
            // Vérification si la Raison Sociale est unique
            if (_context.Societes.Any(s => s.RaisonSociale_Societe == societe.RaisonSociale_Societe))
            {
                return $"Erreur: La Raison Sociale '{societe.RaisonSociale_Societe}' est déjà utilisée.";
            }

            await _context.AddAsync(societe);
            await _context.SaveChangesAsync();
            return "Société ajoutée avec succès.";
        }

        public async Task<string> UpdateSociete(SocieteModel societe)
        {
            // Vérification si le Matricule Fiscal existe déjà (pour éviter les doublons)
            if (_context.Societes.Any(s => s.MF_Societe == societe.MF_Societe && s.ID_Societe != societe.ID_Societe))
            {
                return $"Erreur: Le Matricule Fiscal '{societe.MF_Societe}' est déjà utilisé.";
            }
            // Vérification si la Raison Sociale est unique
            if (_context.Societes.Any(s => s.RaisonSociale_Societe == societe.RaisonSociale_Societe && s.ID_Societe != societe.ID_Societe))
            {
                return $"Erreur: La Raison Sociale '{societe.RaisonSociale_Societe}' est déjà utilisée.";
            }

            _context.Update(societe);
            await _context.SaveChangesAsync();
            return "Société modifiée avec succès.";
        }

        public async Task<string> DeleteSociete(SocieteModel societe)
        {
            _context.Remove(societe);
            await _context.SaveChangesAsync();
            return "Société supprimée avec succès.";
        }

        public async Task<IEnumerable<SocieteModel>> GetSocieteByRS(string rs)
        {
            return await _context.Societes
                .Where(s => s.RaisonSociale_Societe.Contains(rs))
                .ToListAsync();
        }

        public async Task<IEnumerable<SocieteModel>> GetSocieteByMF(string mf)
        {
            return await _context.Societes
                .Where(s => s.MF_Societe.Contains(mf))
                .ToListAsync();
        }
    }
}
