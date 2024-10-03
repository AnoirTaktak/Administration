using Administration.Models;

namespace Administration.Services
{
    public interface IClient_Service
    {
        Task<IEnumerable<Client>> GetAllClients();
        Task<Client> GetClientById(int id);
        Task<Client> AddClient(Client client);
        Client UpdateClient(Client client);
        Client DeleteClient(Client client);
        Task<Client> GetClientByRS(string rs); // Méthode pour obtenir un client par raison sociale
    }
}
