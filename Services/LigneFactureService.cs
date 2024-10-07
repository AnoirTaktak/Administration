using Administration.Dtos;
using Administration.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Administration.Services
{
    public class LigneFactureService : ILigneFactureService
    {
        private readonly AppDBContext _context;

        public LigneFactureService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LigneFacture>> GetAllLignesFacture()
        {
            return await _context.LignesFacture.ToListAsync();
        }

        public async Task<LigneFacture> CreateLigneFacture(LigneFactureDto ligneFactureDto)
        {
            // Recherche du service correspondant à l'ID_Service
            var service = await _context.Services.FindAsync(ligneFactureDto.ID_Service);

            // Vérifier si le service existe
            if (service == null)
            {
                throw new ArgumentException($"Service with ID {ligneFactureDto.ID_Service} not found.");
            }

            // Calcul du total de la ligne (PrixTTC * Quantité)
            var totalLigne = service.PrixTTC * ligneFactureDto.Quantite;

            // Création de la ligne de facture
            var ligneFacture = new LigneFacture
            {
                ID_Service = ligneFactureDto.ID_Service,
                Quantite = ligneFactureDto.Quantite,
                ID_FactureVente = ligneFactureDto.ID_FactureVente,
                Total_LigneFV = totalLigne // Affectation du total calculé
            };

            // Ajout de la ligne de facture à la base de données
            _context.LignesFacture.Add(ligneFacture);
            await _context.SaveChangesAsync();

            return ligneFacture;
        }

        public async Task<IEnumerable<LigneFacture>> GetLignesByFacture(string factureNum)
        {
            return await _context.LignesFacture
                .Where(l => _context.FacturesVente.FindAsync("NumeroFacture",l.ID_FactureVente).Equals(factureNum))
                .ToListAsync();
        }
    }
}
