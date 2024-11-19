using Administration.Dtos;
using Administration.Models;
using Microsoft.EntityFrameworkCore;
using LigneFactureVenteModel = Administration.Models.LigneFacture; // Création de l'alias


namespace Administration.Services.LigneFacture
{
    public class LigneFactureService : ILigneFactureService
    {
        private readonly AppDBContext _context;

        public LigneFactureService(AppDBContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<LigneFactureVenteModel>> GetAllLignesFacture()
        {
            return await _context.LignesFacture.ToListAsync();
        }


        public async Task<LigneFactureVenteModel> CreateLigneFacture(LigneFactureDto ligneFactureDto)
        {

            var service = await _context.Services.FindAsync(ligneFactureDto.ID_Service);


            if (service == null)
            {
                throw new ArgumentException($"Service with ID {ligneFactureDto.ID_Service} not found.");
            }

            // Vérifier que la quantité est valide (> 0)
            if (ligneFactureDto.Quantite <= 0)
            {
                throw new ArgumentException("Quantity must be greater than 0.");
            }

            // Calcul du total TTC de la ligne (PrixTTC * Quantité)
            var totalLigneTTC = service.PrixTTC * ligneFactureDto.Quantite;

            // Calcul du total HT à partir du total TTC et du taux de TVA du service
            var totalLigneHT = totalLigneTTC / (1 + service.TVA / 100);


            var ligneFacture = new LigneFactureVenteModel
            {
                ID_Service = ligneFactureDto.ID_Service,
                Quantite = ligneFactureDto.Quantite,
                ID_FactureVente = ligneFactureDto.ID_FactureVente,
                Total_LigneFV = totalLigneTTC,
                Total_LigneHT = totalLigneHT
            };


            _context.LignesFacture.Add(ligneFacture);
            await _context.SaveChangesAsync();

            return ligneFacture;
        }


        public async Task<List<LigneFactureVenteModel>> GetLignesByFacture(string factureNum)
        {

            var facture = await _context.FacturesVente
                                        .Where(f => f.NumeroFacture == factureNum)
                                        .FirstOrDefaultAsync();

            if (facture == null)
            {
                throw new ArgumentException($"Facture with NumeroFacture {factureNum} not found.");
            }


            var lignesFacture = await _context.LignesFacture
                                              .Where(lf => lf.ID_FactureVente == facture.ID_FactureVente)
                                              .ToListAsync();

            return lignesFacture;
        }

        public async Task<List<LigneFactureVenteModel>> GetLignesByFactureId(int factureId)
        {
            // Récupérer les lignes de facture associées à la facture par ID
            var lignesFacture = await _context.LignesFacture
                                              .Where(lf => lf.ID_FactureVente == factureId)
                                              .ToListAsync();

            if (lignesFacture == null || !lignesFacture.Any())
            {
                throw new ArgumentException($"Aucune ligne de facture trouvée pour l'ID de facture {factureId}.");
            }

            return lignesFacture;
        }

        public async Task<LigneFactureVenteModel> UpdateLigneFacture(int id, LigneFactureDto ligneFactureDto)
        {

            var ligneFacture = await _context.LignesFacture.FindAsync(id);
            if (ligneFacture == null)
            {
                throw new ArgumentException($"Ligne de facture avec ID {id} non trouvée.");
            }


            var service = await _context.Services.FindAsync(ligneFactureDto.ID_Service);
            if (service == null)
            {
                throw new ArgumentException($"Service with ID {ligneFactureDto.ID_Service} not found.");
            }


            if (ligneFactureDto.Quantite <= 0)
            {
                throw new ArgumentException("Quantity must be greater than 0.");
            }


            ligneFacture.ID_Service = ligneFactureDto.ID_Service;
            ligneFacture.Quantite = ligneFactureDto.Quantite;


            ligneFacture.Total_LigneFV = service.PrixTTC * ligneFactureDto.Quantite;
            ligneFacture.Total_LigneHT = ligneFacture.Total_LigneFV / (1 + service.TVA / 100);


            await _context.SaveChangesAsync();

            return ligneFacture;
        }

    }
}
