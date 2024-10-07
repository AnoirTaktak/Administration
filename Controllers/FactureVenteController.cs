using Administration.Dtos;
using Administration.Models;
using Administration.Services;
using Microsoft.AspNetCore.Mvc;

namespace Administration.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FactureVenteController : ControllerBase
    {
        private readonly IFactureVente_Service _factureVenteService;

        public FactureVenteController(IFactureVente_Service factureVenteService)
        {
            _factureVenteService = factureVenteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFactures()
        {
            var factures = await _factureVenteService.GetAllFactures();
            return Ok(factures);
        }

        [HttpGet("numero/{numeroFacture}")]
        public async Task<IActionResult> GetFactureByNumero(string numeroFacture)
        {
            var facture = await _factureVenteService.GetFactureByNumero(numeroFacture);
            if (facture == null)
            {
                return NotFound();
            }
            return Ok(facture);
        }

        [HttpGet("client/{clientId}")]
        public async Task<IActionResult> GetFacturesByClient(int clientId)
        {
            var factures = await _factureVenteService.GetFacturesByClient(clientId);
            return Ok(factures);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFacture([FromBody] FactureVenteDto factureVenteDto)
        {
            if (factureVenteDto == null)
            {
                return BadRequest("Facture data is null.");
            }
            var facture = await _factureVenteService.CreateFacture(factureVenteDto);
            return CreatedAtAction(nameof(GetFactureByNumero), new { numeroFacture = facture.NumeroFacture }, facture);
        }
    }
}
