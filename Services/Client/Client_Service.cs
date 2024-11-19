using Administration.Dtos;
using Administration.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ClientModel = Administration.Models.Client; // Création de l'alias


namespace Administration.Services.Client
{
    public class Client_Service : IClient_Service
    {
        private readonly AppDBContext _context;

        public Client_Service(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClientModel>> GetAllClients()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<ClientModel> GetClientById(int id)
        {
            var client = await _context.Clients.SingleOrDefaultAsync(c => c.ID_Client == id);

            if (client == null)
            {
                throw new ArgumentException($"Client avec ID : '{id}' n'existe pas.");
            }

            return client;
        }

        public async Task<string> AddClient(ClientModel client)
        {
            // Vérifier si RS_Client existe déjà
            if (_context.Clients.Any(c => c.RS_Client == client.RS_Client))
            {
                return "Erreur: La Raison Sociale '" + client.RS_Client + "' existe déjà.";
            }

            // Vérifier si MF_Client existe déjà
            if (_context.Clients.Any(c => c.MF_Client == client.MF_Client))
            {
                return "Erreur: Le Matricule Fiscal '" + client.MF_Client + "' existe déjà.";
            }

            // Si tout est valide, ajouter le client
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return "Client ajouté avec succès.";
        }


        public string UpdateClient(ClientModel client)
        {
            // Vérifier si RS_Client existe déjà pour un autre client
            if (_context.Clients.Any(c => c.RS_Client == client.RS_Client && c.ID_Client != client.ID_Client))
            {
                return "Erreur: La Raison Sociale " + client.RS_Client + " existe déjà pour un autre client.";
            }

            // Vérifier si MF_Client existe déjà pour un autre client
            if (_context.Clients.Any(c => c.MF_Client == client.MF_Client && c.ID_Client != client.ID_Client))
            {
                return "Erreur: Le Matricule Fiscal " + client.MF_Client + " existe déjà pour un autre client.";
            }

            // Mettre à jour le client
            _context.Update(client);
            _context.SaveChanges();

            return "Client modifié avec succès.";

        }

        public ClientModel DeleteClient(ClientModel client)
        {
            _context.Remove(client);
            _context.SaveChanges();

            return client;
        }

        public async Task<IEnumerable<ClientModel>> GetClientByRS(string rsClient)
        {
            var clients = await _context.Clients
                                .Where(c => c.RS_Client.Contains(rsClient))
                                .ToListAsync();
            return clients;
        }

        public async Task<IEnumerable<ClientModel>> GetClientByMF(string mfClient)
        {
            var clients = await _context.Clients
                                 .Where(c => c.MF_Client.Contains(mfClient))
                                 .ToListAsync();
            return clients;
        }

    }
}
