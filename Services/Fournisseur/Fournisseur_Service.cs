using Administration.Models;
using Microsoft.EntityFrameworkCore;
using FournisseurModel = Administration.Models.Fournisseur; // Création de l'alias


namespace Administration.Services.Fournisseur
{
    public class Fournisseur_Service : IFournisseur_Service
    {
        private readonly AppDBContext _context;

        public Fournisseur_Service(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FournisseurModel>> GetAllFournisseurs()
        {
            return await _context.Fournisseurs.ToListAsync();
        }

        public async Task<FournisseurModel> GetFournisseurById(int id)
        {
            var four = await _context.Fournisseurs.SingleOrDefaultAsync(f => f.ID_Fournisseur == id);

            if (four == null)
            {
                throw new ArgumentException($"Fournisseur avec ID : '{id}' n'existe pas.");
            }

            return four;
        }

        public async Task<string> AddFournisseur(FournisseurModel fournisseur)
        {
            // Vérifier si le MF existe déjà
            if (_context.Fournisseurs.Any(f => f.MF_Fournisseur == fournisseur.MF_Fournisseur))
            {
                return "Erreur: Le Matricule fiscale est déja utilisé '" + fournisseur.MF_Fournisseur + "'.";
            }

            // Vérifier si RS existe déjà
            if (_context.Fournisseurs.Any(f => f.RaisonSociale_Fournisseur == fournisseur.RaisonSociale_Fournisseur))
            {
                return "Erreur: Le Raison sociale est déja utilisé '" + fournisseur.RaisonSociale_Fournisseur + "'.";
            }

            await _context.AddAsync(fournisseur);
            _context.SaveChanges();
            return "Fournisseur ajouté avec succès.";
        }

        public string UpdateFournisseur(FournisseurModel fournisseur)
        {
            // Vérifier si le MF existe déjà
            if (_context.Fournisseurs.Any(f => f.MF_Fournisseur == fournisseur.MF_Fournisseur && f.ID_Fournisseur != fournisseur.ID_Fournisseur))
            {
                return "Erreur: Le Matricule fiscale est déja utilisé '" + fournisseur.MF_Fournisseur + "'.";
            }

            // Vérifier si RS existe déjà
            if (_context.Fournisseurs.Any(f => f.RaisonSociale_Fournisseur == fournisseur.RaisonSociale_Fournisseur && f.ID_Fournisseur != fournisseur.ID_Fournisseur))
            {
                return "Erreur: Le Raison sociale est déja utilisé '" + fournisseur.RaisonSociale_Fournisseur + "'.";
            }

            _context.Update(fournisseur);
            _context.SaveChanges();
            return "Fournisseur modifié avec succès.";
        }

        public FournisseurModel DeleteFournisseur(FournisseurModel fournisseur)
        {

            _context.Remove(fournisseur);
            _context.SaveChanges();
            return fournisseur;

        }

        public async Task<IEnumerable<FournisseurModel>> GetFournisseurByRS(string rs)
        {
            var four = await _context.Fournisseurs.Where(f => f.RaisonSociale_Fournisseur.Contains(rs)).ToListAsync();
           
            return four;
        }

        public async Task<IEnumerable<FournisseurModel>> GetFournisseurByMF(string mf)
        {
            var four = await _context.Fournisseurs.Where(f => f.MF_Fournisseur.Contains(mf)).ToListAsync();
            
            return four;
        }
    }
}
