using Administration.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RetenuetModel = Administration.Models.Retenue; // Création de l'alias


namespace Administration.Services.Retenue
{
    public class Retenue_Service : IRetenue_Service
    {
        private readonly AppDBContext _context;

        public Retenue_Service(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RetenuetModel>> GetAllRetenuesAsync()
        {
            return await _context.Retenues.ToListAsync();
        }

        public async Task<RetenuetModel> GetRetenueByIdAsync(int id)
        {
            return await _context.Retenues.SingleOrDefaultAsync(r => r.ID_Retenue == id);
        }

        public async Task<string> AddRetenueAsync(RetenuetModel retenue)
        {
            await _context.Retenues.AddAsync(retenue);
            await _context.SaveChangesAsync();
            return "Retenue ajoutée avec succès.";
        }

        public async Task<string> UpdateRetenueAsync(RetenuetModel retenue)
        {
            _context.Retenues.Update(retenue);
            await _context.SaveChangesAsync();
            return "Retenue mise à jour avec succès.";
        }

        public async Task<RetenuetModel> DeleteRetenueAsync(RetenuetModel retenue)
        {
            _context.Retenues.Remove(retenue);
            await _context.SaveChangesAsync();
            return retenue;
        }

        public async Task<IEnumerable<RetenuetModel>> GetRetenuesByFactureAchatIdAsync(int factureAchatId)
        {
            return await _context.Retenues
                .Where(r => r.ID_FactureAchat == factureAchatId)
                .ToListAsync();
        }

        public async Task<IEnumerable<RetenuetModel>> GetRetenuesByFournisseurIdAsync(int fournisseurId)
        {
            // Ici, vous devez lier la table Retenue avec la table FactureAchat pour obtenir les retenues par fournisseur
            return await _context.Retenues
                .Where(r => _context.FacturesAchat.Any(f => f.ID_FactureAchat == r.ID_FactureAchat && f.ID_Fournisseur == fournisseurId))
                .ToListAsync();
        }
    }
}
