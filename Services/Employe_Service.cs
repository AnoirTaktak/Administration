using Administration.Models;
using Microsoft.EntityFrameworkCore;

namespace Administration.Services
{
    public class Employe_Service : IEmploye_Service
    {
        private readonly AppDBContext _context;

        public Employe_Service(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employe>> GetAllEmployes()
        {
            return await _context.Employes.ToListAsync();
        }

        public async Task<Employe> GetEmployeById(int id)
        {
            return await _context.Employes.SingleOrDefaultAsync(e => e.ID_Employe == id);
        }

        public async Task<Employe> AddEmploye(Employe employe)
        {
            await _context.AddAsync(employe);
            _context.SaveChanges();
            return employe;
        }

        public Employe UpdateEmploye(Employe employe)
        {
            _context.Update(employe);
            _context.SaveChanges();
            return employe;
        }

        public Employe DeleteEmploye(Employe employe)
        {
            _context.Remove(employe);
            _context.SaveChanges();
            return employe;
        }

        public async Task<Employe> GetEmployeByNom(string nom)
        {
            return await _context.Employes.FirstOrDefaultAsync(e => e.Nom_Employe == nom);
        }

        public async Task<IEnumerable<Employe>> GetEmployesByTypeContrat(string typeContrat)
        {
            return await _context.Employes.Where(e => e.TypeContrat == typeContrat).ToListAsync();
        }
    }
}
