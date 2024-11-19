using Administration.Dtos;
using Administration.Models;
using Administration.Services.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Administration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClient_Service _client_Service;

        public ClientController(IClient_Service client_Service)
        {
            _client_Service = client_Service;
        }
     

        [HttpGet]
        public async Task<IActionResult> GetAllClientsAsync()
        {
            var clients = await _client_Service.GetAllClients();
            return Ok(clients);
        }
        

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientByIdAsync(int id)
        {
            var client = await _client_Service.GetClientById(id);
            if (client == null)
            {
                return NotFound("Client introuvable");
            }
            return Ok(client);
        }
      

        [HttpGet("rs/{rs}")]
        public async Task<IActionResult> GetClientByRSAsync(string rs)
        {
            var client = await _client_Service.GetClientByRS(rs);
            if (client == null)
            {
                return NotFound("Client introuvable");
            }
            return Ok(client);
        }

        [HttpGet("mf/{mf}")]
        public async Task<IActionResult> GetClientByMFAsync(string mf)
        {
            var client = await _client_Service.GetClientByMF(mf);
            if (client == null)
            {
                return NotFound("Client introuvable");
            }
            return Ok(client);
        }


        
        [HttpPost]
        public async Task<IActionResult> CreateClientAsync(ClientDto clientDto)
        {
            var client = new Client
            {
                MF_Client = clientDto.MF_Client,
                RS_Client = clientDto.RS_Client,
                Adresse_Client = clientDto.Adresse_Client,
                Tel_Client = clientDto.Tel_Client,
                Type_Client = clientDto.Type_Client
            };

            // Appel du service pour ajouter le client
            var result = await _client_Service.AddClient(client);

            // Si un message d'erreur est retourné, renvoyer un code 400 avec le message d'erreur
            if (result.StartsWith("Erreur"))
            {
                return BadRequest(result);
            }

            // Sinon, renvoyer un code 201 pour succès
            return Ok($"Client {client.RS_Client} crée avec succés");
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClientAsync(int id, ClientDto clientDto)
        {
            var client = await _client_Service.GetClientById(id);
            if (client == null)
            {
                return NotFound(" Client Introuvable ");
            }
            client.Adresse_Client = clientDto.Adresse_Client;
            client.MF_Client = clientDto.MF_Client;
            client.RS_Client = clientDto.RS_Client;
            client.Tel_Client = clientDto.Tel_Client;
            client.Type_Client = clientDto.Type_Client; 

            var result = _client_Service.UpdateClient(client);

            // Si un message d'erreur est retourné, renvoyer un code 400 avec le message d'erreur
            if (result.StartsWith("Erreur"))
            {
                return BadRequest(result);
            }

            return Ok($"Le client '{client.RS_Client}' a été Modifié avec succès.");
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClientAsync(int id)
        {
            var client = await _client_Service.GetClientById(id);
            if (client == null)
            {
                return NotFound("Client introuvable pour suppression");
            }
            _client_Service.DeleteClient(client);
            return Ok($"Le client '{client.RS_Client}' a été supprimé avec succès.");
        }
    }
}
