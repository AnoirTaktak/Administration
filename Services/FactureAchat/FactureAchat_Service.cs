using Administration.Models;
using Microsoft.EntityFrameworkCore;
using FactureAchatModel = Administration.Models.FactureAchat; // Création de l'alias


namespace Administration.Services.FactureAchat
{
    public class FactureAchat_Service : IFactureAchat_Service
    {
        private readonly AppDBContext _context;

        public FactureAchat_Service(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FactureAchatModel>> GetAllFacturesAchat()
        {
            return await _context.FacturesAchat
                .OrderBy(f => f.DateAchat) // Trier par date
                .ToListAsync();
        }

        public async Task<FactureAchatModel> GetFactureAchatById(int id)
        {
            var facture = await _context.FacturesAchat.SingleOrDefaultAsync(f => f.ID_FactureAchat == id);
            if (facture == null)
            {
                throw new ArgumentException($"Facture avec ID : '{id}' n'existe pas.");
            }
            return facture;
        }

        public async Task<IEnumerable<FactureAchatModel>> GetFacturesByDateRangeAsync(DateOnly? startDate, DateOnly? endDate)
        {
            var query = _context.FacturesAchat.AsQueryable();

            if (startDate.HasValue)
                query = query.Where(f => f.DateAchat >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(f => f.DateAchat <= endDate.Value);

            return await query.ToListAsync();
        }

        public async Task<string> AddFactureAchat(FactureAchatModel factureAchat)
        {
            if (factureAchat.Montant <= 0)
            {
                return "Erreur: Le montant doit être supérieur à zéro.";
            }

            await _context.AddAsync(factureAchat);
            await _context.SaveChangesAsync();
            return "Facture d'achat ajoutée avec succès.";
        }

        public async Task<string> UpdateFactureAchat(FactureAchatModel factureAchat)
        {
            if (factureAchat.Montant <= 0)
            {
                return "Erreur: Le montant doit être supérieur à zéro.";
            }

            _context.Update(factureAchat);
            await _context.SaveChangesAsync();
            return "Facture d'achat modifiée avec succès.";
        }

        public async Task<FactureAchatModel> DeleteFactureAchat(FactureAchatModel factureAchat)
        {
            _context.Remove(factureAchat);
            await _context.SaveChangesAsync();
            return factureAchat;
        }

        public async Task<IEnumerable<FactureAchatModel>> GetFacturesAchatByFournisseur(int fournisseurId)
        {
            return await _context.FacturesAchat
                .Where(f => f.ID_Fournisseur == fournisseurId)
                .ToListAsync();
        }

        public async Task<IEnumerable<FactureAchatModel>> GetFacturesAchatByEtat(bool etatPaiement)
        {
            return await _context.FacturesAchat
                .Where(f => f.EtatPaiement == etatPaiement)
                .ToListAsync();
        }

        public async Task<IEnumerable<FactureAchatModel>> GetFacturesAchatByNumFac(string numfac)
        {
            var fa = await _context.FacturesAchat.Where(e => e.Numero_FactureAchat.Contains(numfac)).ToListAsync();

            return fa;
        }
        
    }
}
