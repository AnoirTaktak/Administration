using Administration.Models;
using SocieteModel = Administration.Models.Societe; // Création de l'alias


namespace Administration.Services.Societe
{
    public interface ISociete_Service
    {
        Task<IEnumerable<SocieteModel>> GetAllSocietes();
        Task<SocieteModel> GetSocieteById(int id);
        Task<string> AddSociete(SocieteModel societe);
        Task<string> UpdateSociete(SocieteModel societe);
        Task<string> DeleteSociete(SocieteModel societe);
        Task<IEnumerable<SocieteModel>> GetSocieteByMF(string mf);
        Task<IEnumerable<SocieteModel>> GetSocieteByRS(string rs);
    }
}
