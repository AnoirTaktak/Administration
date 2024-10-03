using Administration.Dtos;
using Administration.Models;
using Administration.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllClientsAsync()
        {
            var clients = await _client_Service.GetAllClients();
            return Ok(clients);
        }
        [Authorize]
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
        [Authorize]
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
        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpPost]
        public async Task<IActionResult> CreateClientAsync(ClientDto clientDto)
        {
            var client = new Client
            {
                MF_Client = clientDto.MF_Client,
                RS_Client = clientDto.RS_Client,
                Adresse_Client = clientDto.Adresse_Client,
                Tel_Client = clientDto.Tel_Client,
                Type_Client = clientDto.Type_Client,
            };

            await _client_Service.AddClient(client);
            return CreatedAtAction(nameof(GetClientByIdAsync), new { id = client.ID_Client }, client);
        }
        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClientAsync(int id, ClientDto clientDto)
        {
            var client = await _client_Service.GetClientById(id);
            if (client == null)
            {
                return NotFound("Client introuvable");
            }

            client.MF_Client = clientDto.MF_Client;
            client.RS_Client = clientDto.RS_Client;
            client.Adresse_Client = clientDto.Adresse_Client;
            client.Tel_Client = clientDto.Tel_Client;
            client.Type_Client = clientDto.Type_Client;

            _client_Service.UpdateClient(client);
            return Ok(client);
        }
        [Authorize(Roles = "SuperAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClientAsync(int id)
        {
            var client = await _client_Service.GetClientById(id);
            if (client == null)
            {
                return NotFound("Client introuvable pour suppression");
            }
            _client_Service.DeleteClient(client);
            return NoContent(); // 204 No Content
        }
    }
}
