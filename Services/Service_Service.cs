using Administration.Models;
using Microsoft.EntityFrameworkCore;

namespace Administration.Services
{
    public class Service_Service : IService_Service
    {
        private readonly AppDBContext _context;

        public Service_Service(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Service>> GetAllServices()
        {
            return await _context.Services.ToListAsync();
        }

        public async Task<Service> GetServiceById(int id)
        {
            return await _context.Services.SingleOrDefaultAsync(s => s.ID_Service == id);
        }

        public async Task<Service> AddService(Service service)
        {
            await _context.AddAsync(service);
            _context.SaveChanges();
            return service;
        }

        public Service UpdateService(Service service)
        {
            _context.Update(service);
            _context.SaveChanges();
            return service;
        }

        public Service DeleteService(Service service)
        {

            _context.Remove(service);
            _context.SaveChanges();
            return service;

        }

        public async Task<Service> GetServiceByDes(string des)
        {
            return _context.Services.FirstOrDefault(s => s.Designation_Service == des);
        }
    }
}
