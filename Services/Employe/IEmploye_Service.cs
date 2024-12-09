using Administration.Models;
using EmployeModel = Administration.Models.Employe; // Création de l'alias


namespace Administration.Services.Employe
{
    public interface IEmploye_Service
    {

        Task<IEnumerable<EmployeModel>> GetAllEmployes();
        Task<EmployeModel> GetEmployeById(int id);
        Task<string> AddEmploye(EmployeModel employe);
        Task<string> UpdateEmploye(EmployeModel employe);
        Task<string> DeleteEmploye(EmployeModel employe);
        Task<IEnumerable<EmployeModel>> GetEmployeByNom(string nom);
        Task<IEnumerable<EmployeModel>> GetEmployesByTypeContrat(TypeContrat typecontrat);
        Task<IEnumerable<EmployeModel>> GetEmployeByCin(string cin);
    }
}
