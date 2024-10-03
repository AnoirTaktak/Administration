using Administration.Models;

namespace Administration.Services
{
    public interface IEmploye_Service
    {

        Task<IEnumerable<Employe>> GetAllEmployes();
        Task<Employe> GetEmployeById(int id);
        Task<Employe> AddEmploye(Employe employe);
        Employe UpdateEmploye(Employe employe);
        Employe DeleteEmploye(Employe employe);
        Task<Employe> GetEmployeByNom(string nom);
        Task<IEnumerable<Employe>> GetEmployesByTypeContrat(string typecontrat);
    }
}
