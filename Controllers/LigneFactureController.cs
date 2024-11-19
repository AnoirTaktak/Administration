using Administration.Dtos;
using Administration.Models;
using Administration.Services.LigneFacture;
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

 

        [HttpGet]
        public async Task<IActionResult> GetAllLignesFactureAsync()
        {
            var lignes = await _ligneFactureService.GetAllLignesFacture();
            return Ok(lignes);
        }

        

        [HttpPost]
        public async Task<IActionResult> CreateLignesFactureAsync([FromBody] List<LigneFactureDto> lignesFactureDto)
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

       

        [HttpGet("lignefactureNum/{factureNumero}")]
        public async Task<IActionResult> GetLignesByFactureAsync(string factureNumero)
        {
            var lignes = await _ligneFactureService.GetLignesByFacture(factureNumero);

            if (lignes == null || !lignes.Any())
            {
                return NotFound($"No lignes found for facture {factureNumero}.");
            }

            return Ok(lignes);
        }


        [HttpGet("lignefactureId/{factureid}")]
        public async Task<IActionResult> GetLignesByFactureIdAsync(int factureid)
        {
            var lignes = await _ligneFactureService.GetLignesByFactureId(factureid);

            if (lignes == null || !lignes.Any())
            {
                return NotFound($"No lignes found for facture {factureid}.");
            }

            return Ok(lignes);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLigneFactureAsync(int id, [FromBody] LigneFactureDto ligneFactureDto)
        {
            if (ligneFactureDto == null)
            {
                return BadRequest("Ligne de facture data is null.");
            }

            try
            {
                var updatedLigne = await _ligneFactureService.UpdateLigneFacture(id, ligneFactureDto);
                return Ok(updatedLigne);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
