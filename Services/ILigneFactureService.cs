using Administration.Dtos;
using Administration.Models;

namespace Administration.Services
{
    public interface ILigneFactureService
    {
        Task<IEnumerable<LigneFacture>> GetAllLignesFacture();
        Task<LigneFacture> CreateLigneFacture(LigneFactureDto ligneFactureDto);
        Task<IEnumerable<LigneFacture>> GetLignesByFacture(string factureId);


    }
}
