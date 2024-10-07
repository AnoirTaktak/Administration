using Administration.Dtos;
using Administration.Models;
using Administration.Services;
using Microsoft.AspNetCore.Mvc;

namespace Administration.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LigneFactureController : ControllerBase
    {
        private readonly ILigneFactureService _ligneFactureService;

        public LigneFactureController(ILigneFactureService ligneFactureService)
        {
            _ligneFactureService = ligneFactureService;
        }

        // Récupérer toutes les lignes de facture
        [HttpGet]
        public async Task<IActionResult> GetAllLignesFacture()
        {
            var lignes = await _ligneFactureService.GetAllLignesFacture();
            return Ok(lignes);
        }

        // Créer une ligne de facture
        [HttpPost]
        public async Task<IActionResult> CreateLignesFacture([FromBody] List<LigneFactureDto> lignesFactureDto)
        {
            if (lignesFactureDto == null || !lignesFactureDto.Any())
            {
                return BadRequest("Ligne de facture data is null or empty.");
            }

            foreach (var ligneFactureDto in lignesFactureDto)
            {
                await _ligneFactureService.CreateLigneFacture(ligneFactureDto);
            }

            return Ok(lignesFactureDto);
        }

        // Récupérer les lignes de facture par numéro de facture
        [HttpGet("facture/{factureNumero}")]
        public async Task<IActionResult> GetLignesByFacture(string factureNumero)
        {
            var lignes = await _ligneFactureService.GetLignesByFacture(factureNumero);

            if (lignes == null || !lignes.Any())
            {
                return NotFound($"No lignes found for facture {factureNumero}.");
            }

            return Ok(lignes);
        }
    }
}
