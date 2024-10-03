using Administration.Models;
using Microsoft.EntityFrameworkCore;

namespace Administration.Services
{
    public class Fournisseur_Service : IFournisseur_Service
    {
        private readonly AppDBContext _context;

        public Fournisseur_Service(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Fournisseur>> GetAllFournisseurs()
        {
            return await _context.Fournisseurs.ToListAsync();
        }

        public async Task<Fournisseur> GetFournisseurById(int id)
        {
            return await _context.Fournisseurs.SingleOrDefaultAsync(f => f.ID_Fournisseur == id);
        }

        public async Task<Fournisseur> AddFournisseur(Fournisseur fournisseur)
        {
            await _context.AddAsync(fournisseur);
            _context.SaveChanges();
            return fournisseur;
        }

        public Fournisseur UpdateFournisseur(Fournisseur fournisseur)
        {
            _context.Update(fournisseur);
            _context.SaveChanges();
            return fournisseur;
        }

        public Fournisseur DeleteFournisseur(Fournisseur fournisseur)
        {

            _context.Remove(fournisseur);
            _context.SaveChanges();
            return fournisseur;

        }

        public async Task<Fournisseur> GetFournisseurByRS(string rs)
        {
            return _context.Fournisseurs.FirstOrDefault(f => f.RaisonSociale_Fournisseur == rs);
        }
    }
}
