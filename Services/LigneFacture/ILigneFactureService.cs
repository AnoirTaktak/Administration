using Administration.Dtos;
using Administration.Models;
using LigneFactureVenteModel = Administration.Models.LigneFacture; // Création de l'alias


namespace Administration.Services.LigneFacture
{
    public interface ILigneFactureService
    {
        Task<IEnumerable<LigneFactureVenteModel>> GetAllLignesFacture();
        Task<LigneFactureVenteModel> CreateLigneFacture(LigneFactureDto ligneFactureDto);
        Task<List<LigneFactureVenteModel>> GetLignesByFacture(string factureNum); // Méthode  pour récupérer par NumeroFacture
        Task<List<LigneFactureVenteModel>> GetLignesByFactureId(int factureId); // Méthode  pour récupérer par ID
        Task<LigneFactureVenteModel> UpdateLigneFacture(int id, LigneFactureDto ligneFactureDto); // Méthode  pour mettre à jour

    }
}
