using Administration.Models;
using Microsoft.EntityFrameworkCore;

namespace Administration.Services
{
    public class Societe_Service : ISociete_Service
    {
        private readonly AppDBContext _context;

        public Societe_Service(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Societe>> GetAllSocietes()
        {
            return await _context.Societes.ToListAsync();
        }

        public async Task<Societe> GetSocieteById(int id)
        {
            return await _context.Societes.SingleOrDefaultAsync(s => s.ID_Societe == id);
        }

        public async Task<Societe> AddSociete(Societe societe)
        {
            await _context.AddAsync(societe);
            await _context.SaveChangesAsync();
            return societe;
        }

        public Societe UpdateSociete(Societe societe)
        {
            _context.Update(societe);
            _context.SaveChanges();
            return societe;
        }

        public Societe DeleteSociete(Societe societe)
        {
            _context.Remove(societe);
            _context.SaveChanges();
            return societe;
        }
    }
}
