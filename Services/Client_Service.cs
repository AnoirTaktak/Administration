using Administration.Models;
using Microsoft.EntityFrameworkCore;

namespace Administration.Services
{
    public class Client_Service : IClient_Service
    {
        private readonly AppDBContext _context;

        public Client_Service(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Client>> GetAllClients()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<Client> GetClientById(int id)
        {
            return await _context.Clients.SingleOrDefaultAsync(c => c.ID_Client == id);
        }

        public async Task<Client> AddClient(Client client)
        {
            await _context.AddAsync(client);
            await _context.SaveChangesAsync();
            return client;
        }

        public Client UpdateClient(Client client)
        {
            _context.Update(client);
            _context.SaveChanges();
            return client;
        }

        public Client DeleteClient(Client client)
        {
            _context.Remove(client);
            _context.SaveChanges();
            return client;
        }

        public async Task<Client> GetClientByRS(string rs)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.RS_Client == rs);
        }
    }
}
