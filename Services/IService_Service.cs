using Administration.Models;

namespace Administration.Services
{
    public interface IService_Service
    {
        Task<IEnumerable<Service>> GetAllServices();
        Task<Service> GetServiceById(int id);
        Task<Service> AddService(Service service);
        Service UpdateService(Service service);
        Service DeleteService(Service service);
        Task<Service> GetServiceByDes(string des);
    }
}
