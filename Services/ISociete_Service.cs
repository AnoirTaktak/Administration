using Administration.Models;

namespace Administration.Services
{
    public interface ISociete_Service
    {
        Task<IEnumerable<Societe>> GetAllSocietes();
        Task<Societe> GetSocieteById(int id);
        Task<Societe> AddSociete(Societe societe);
        Societe UpdateSociete(Societe societe);
        Societe DeleteSociete(Societe societe);
    }
}
