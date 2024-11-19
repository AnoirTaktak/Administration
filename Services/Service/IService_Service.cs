using Administration.Models;
using ServiceModel = Administration.Models.Service; // Création de l'alias


public interface IService_Service
{
    Task<IEnumerable<ServiceModel>> GetAllServices();
    Task<ServiceModel> GetServiceById(int id);
    Task<string> AddService(ServiceModel service);
    Task<string> UpdateService(ServiceModel service);  
    Task<ServiceModel> DeleteService(ServiceModel service);   
    Task<IEnumerable<ServiceModel>> GetServiceByDes(string des);
}
