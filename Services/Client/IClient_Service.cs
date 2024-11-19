using Administration.Models;
using ClientModel = Administration.Models.Client; // Création de l'alias


namespace Administration.Services.Client
{
    public interface IClient_Service
    {
 
        Task<IEnumerable<ClientModel>> GetAllClients(); //list de touts les clients
        Task<ClientModel> GetClientById(int id); //recherche client par son ID
        Task<string> AddClient(ClientModel client); //ajouter client
        string UpdateClient(ClientModel client); //modifier client
        ClientModel DeleteClient(ClientModel client); //effacer client
        Task<IEnumerable<ClientModel>> GetClientByRS(string rs); //recherche par raison social
        Task<IEnumerable<ClientModel>> GetClientByMF(string mf); //recherche mar matricule fiscal
    }
}
