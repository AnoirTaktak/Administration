using Administration.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using RetenuetModel = Administration.Models.Retenue; // Création de l'alias


namespace Administration.Services.Retenue
{
    public interface IRetenue_Service
    {
        Task<IEnumerable<RetenuetModel>> GetAllRetenuesAsync();
        Task<RetenuetModel> GetRetenueByIdAsync(int id);
        Task<string> AddRetenueAsync(RetenuetModel retenue);
        Task<string> UpdateRetenueAsync(RetenuetModel retenue);
        Task<RetenuetModel> DeleteRetenueAsync(RetenuetModel retenue);
        Task<IEnumerable<RetenuetModel>> GetRetenuesByFactureAchatIdAsync(int factureAchatId);
        Task<IEnumerable<RetenuetModel>> GetRetenuesByFournisseurIdAsync(int fournisseurId);
    }
}
