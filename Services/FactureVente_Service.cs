using Administration.Dtos;
using Administration.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Administration.Services
{
    public class FactureVente_Service : IFactureVente_Service
    {
        private readonly AppDBContext _context;

        public FactureVente_Service(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FactureVente>> GetAllFactures()
        {
            return await _context.FacturesVente
                .Include(f => f.Client)
                .Include(f => f.LignesFacture)
                .ToListAsync();
        }

        public async Task<FactureVente> GetFactureByNumero(string numeroFacture)
        {
            return await _context.FacturesVente
                .Include(f => f.Client)
                .Include(f => f.LignesFacture)
                .FirstOrDefaultAsync(f => f.NumeroFacture == numeroFacture);
        }

        public async Task<IEnumerable<FactureVente>> GetFacturesByClient(int clientId)
        {
            return await _context.FacturesVente
                .Where(f => f.Client.ID_Client == clientId)
                .Include(f => f.Client)
                .Include(f => f.LignesFacture)
                .ToListAsync();
        }

        public async Task<FactureVente> CreateFacture(FactureVenteDto factureDto)
        {
            decimal totalFacture = 0;

            // Générer le numéro de facture
            string numeroFacture = FactureVente.GenerateNumeroFacture(_context);

            // Créer une nouvelle facture
            var facture = new FactureVente
            {
                NumeroFacture = numeroFacture,  // Initialiser le numéro de facture
                DateFacture = DateTime.Now,
                Total_FactureVente = 0,
                TimbreFiscale = factureDto.TimbreFiscale,
                Client = await _context.Clients.FindAsync(factureDto.Client.ID_Client), // Vérifier que le client existe
                LignesFacture = new List<LigneFacture>() // Initialiser les lignes de facture
            };

            _context.FacturesVente.Add(facture);
            await _context.SaveChangesAsync();

            foreach (var ligneDto in factureDto.LignesFacture)
            {
                var ligneFacture = new LigneFacture
                {
                    ID_Service = ligneDto.ID_Service,
                    Quantite = ligneDto.Quantite,
                    Total_LigneFV = ligneDto.Total_LigneFV,
                    ID_FactureVente = facture.ID_FactureVente  // Associer l'ID de la facture
                };

                // Ajouter chaque ligne à la facture
                facture.LignesFacture.Add(ligneFacture);
                _context.LignesFacture.Add(ligneFacture);

                totalFacture += ligneDto.Total_LigneFV;
            }

            facture.Total_FactureVente = totalFacture;

            _context.FacturesVente.Update(facture);
            // Sauvegarder les lignes de facture avec l'ID_FactureVente associé
            await _context.SaveChangesAsync();

            return facture;
        }
    }
}
