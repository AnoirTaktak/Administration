using Administration.Dtos;
using Administration.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using FactureVenteModel = Administration.Models.FactureVente; // Création de l'alias
using LigneFactureVenteModel = Administration.Models.LigneFacture; // Création de l'alias



namespace Administration.Services.FactureVente
{
    public class FactureVente_Service : IFactureVente_Service
    {
        private readonly AppDBContext _context;

        public FactureVente_Service(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FactureVenteModel>> GetAllFactures()
        {
            return await _context.FacturesVente
                .Include(f => f.Client)
                .Include(f => f.LignesFacture)
                .ToListAsync();
        }

        public async Task<FactureVenteModel> FindByFacId(int facId)
        {
            var fac = await _context.FacturesVente
                .Include(f => f.LignesFacture) // Inclure les lignes de la facture
                .FirstOrDefaultAsync(f => f.ID_FactureVente == facId);

            if (fac == null)
            {
                throw new ArgumentException($"Facture avec ID : '{facId}' n'existe pas.");
            }

            return fac;
        }

        public async Task<FactureVenteModel> GetFactureByNumero(string numeroFacture)
        {
            var fac = await _context.FacturesVente
                .Include(f => f.Client)
                .Include(f => f.LignesFacture)
                .FirstOrDefaultAsync(f => f.NumeroFacture == numeroFacture);

            if (fac == null)
            {
                throw new ArgumentException($" Pas de facture avec ce numero : '{numeroFacture}'.");

            }
            return fac;
        }

        public async Task<IEnumerable<FactureVenteModel>> GetFacturesByClient(int clientId)
        {
            var cl = await _context.Clients.FindAsync(clientId);
            var facs = await _context.FacturesVente
                .Where(f => f.Client.ID_Client == clientId)
                .Include(f => f.Client)
                .Include(f => f.LignesFacture)
                .ToListAsync();

            if (facs == null)
            {
                throw new ArgumentException($" Pas de factures pour ce client : '{cl.RS_Client}'.");

            }
            return facs;
        }

        public async Task<FactureVenteModel> UpdateFacture(int facId, FactureVenteModel updatedFacture)
        {
            var facture = await FindByFacId(facId);
            if (facture == null)
            {
                throw new Exception("Facture introuvable.");
            }

            // Mettre à jour le client si nécessaire
            facture.Client = updatedFacture.Client;

            // Mettre à jour les lignes de facture
            // Supprimer les lignes existantes
            _context.LignesFacture.RemoveRange(facture.LignesFacture);

            // Ajouter les nouvelles lignes
            foreach (var ligne in updatedFacture.LignesFacture)
            {
                // Assurez-vous de créer une nouvelle instance de LigneFacture si nécessaire
                var nouvelleLigne = new LigneFactureVenteModel
                {
                    // Assigner les propriétés nécessaires ici
                    ID_Service = ligne.ID_Service,
                    Quantite = ligne.Quantite,
                    Total_LigneFV = ligne.Total_LigneFV // Calculer si nécessaire
                };
                facture.LignesFacture.Add(nouvelleLigne);
            }

            // Recalculer le total de la facture
            facture.Total_FactureVente = facture.LignesFacture.Sum(l => l.Total_LigneFV);

            await _context.SaveChangesAsync();
            return facture;
        }



        public async Task<FactureVenteModel> CreateFacture(FactureVenteDto factureDto)
        {
            decimal totalFacture = 0;

            // Générer le numéro de facture
            string numeroFacture = FactureVenteModel.GenerateNumeroFacture(_context);

            // Créer une nouvelle facture
            var facture = new FactureVenteModel
            {
                NumeroFacture = numeroFacture,  // Initialiser le numéro de facture
                DateFacture = DateTime.Now,
                Total_FactureVente = 0,
                TimbreFiscale = factureDto.TimbreFiscale,
                Client = await _context.Clients.Where(c => c.RS_Client == factureDto.Client.RS_Client).FirstAsync(), // Vérifier que le client existe
                LignesFacture = new List<LigneFactureVenteModel>() // Initialiser les lignes de facture
            };

            _context.FacturesVente.Add(facture);
            await _context.SaveChangesAsync();

            foreach (var ligneDto in factureDto.LignesFacture)
            {
                var ligneFacture = new LigneFactureVenteModel
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

            facture.Total_FactureVente = totalFacture + facture.TimbreFiscale;

            _context.FacturesVente.Update(facture);
            // Sauvegarder les lignes de facture avec l'ID_FactureVente associé
            await _context.SaveChangesAsync();

            return facture;
        }
    }
}
