using Administration.Models;
using Microsoft.EntityFrameworkCore;
using ServiceModel = Administration.Models.Service; // Création de l'alias


namespace Administration.Services.Service
{
    public class Service_Service : IService_Service
    {
        private readonly AppDBContext _context;

        public Service_Service(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ServiceModel>> GetAllServices()
        {
            return await _context.Services.ToListAsync();
        }

        public async Task<ServiceModel> GetServiceById(int id)
        {
            var service = await _context.Services.SingleOrDefaultAsync(s => s.ID_Service == id);
            if (service == null)
            {
                throw new ArgumentException($"Service avec ID : '{id}' n'existe pas.");
            }

            return service;
        }

        public async Task<string> AddService(ServiceModel service)
        {
            // Vérifier si le designation existe déjà
            if (_context.Services.Any(s => s.Designation_Service == service.Designation_Service))
            {
                return "Erreur: Le Designation service est déja utilisé '" + service.Designation_Service + "'.";
            }

            // Vérifier si RS existe déjà
            if (service.TVA <= 0 || service.PrixTTC <=0)
            {
                return "Erreur: Verifié les valeus tva et prixTTC svp .";
            }

            await _context.AddAsync(service);
            await _context.SaveChangesAsync();
            return "Service ajouté avec succès.";
        }

        public async Task<string> UpdateService(ServiceModel service)
        {
            // Vérifier si le designation existe déjà
            if (_context.Services.Any(s => s.Designation_Service == service.Designation_Service && s.ID_Service != service.ID_Service))
            {
                return "Erreur: Le Designation service est déja utilisé '" + service.Designation_Service + "'.";
            }

            // Vérifier si RS existe déjà
            if (service.TVA <= 0 || service.PrixTTC <= 0)
            {
                return "Erreur: Verifié les valeus tva et prixTTC svp .";
            }
            _context.Update(service);
            await _context.SaveChangesAsync();
            return "Service modifié avec succès.";
        }

        public async Task<ServiceModel> DeleteService(ServiceModel service)
        {
            _context.Remove(service);
            await _context.SaveChangesAsync();
            return service;
        }

        public async Task<IEnumerable<ServiceModel>> GetServiceByDes(string des)
        {
            return await _context.Services
                .Where(s => s.Designation_Service.Contains(des))
                .ToListAsync();
        }
    }
}
