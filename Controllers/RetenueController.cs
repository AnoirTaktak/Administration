using Administration.Dtos;
using Administration.Models;
using Administration.Services.Retenue;
using Microsoft.AspNetCore.Mvc;

namespace Administration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RetenueController : ControllerBase
    {
        private readonly IRetenue_Service _retenueService;

        public RetenueController(IRetenue_Service retenueService)
        {
            _retenueService = retenueService;
        }

        // Ajouter une nouvelle retenue
        [HttpPost]
        public async Task<IActionResult> AddRetenueAsync(RetenueDto retenueDto)
        {
            if (retenueDto == null)
            {
                return BadRequest("Données invalides.");
            }

            var retenue = new Retenue
            {
                Taux = retenueDto.Taux,
                Categorie = retenueDto.Categorie,
                ID_FactureAchat = retenueDto.ID_FactureAchat
            };

            var result = await _retenueService.AddRetenueAsync(retenue);
            return Ok(result);
        }

        // Mettre à jour une retenue
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRetenueAsync(int id, RetenueDto retenueDto)
        {
            if (retenueDto == null)
            {
                return BadRequest("Données invalides.");
            }

            var retenue = await _retenueService.GetRetenueByIdAsync(id);
            if (retenue == null)
            {
                return NotFound("Retenue introuvable.");
            }

            // Mise à jour des propriétés de la retenue
            retenue.Taux = retenueDto.Taux;
            retenue.Categorie = retenueDto.Categorie;
            retenue.ID_FactureAchat = retenueDto.ID_FactureAchat;

            var result = await _retenueService.UpdateRetenueAsync(retenue);
            return Ok(result);
        }

        // Supprimer une retenue
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRetenueAsync(int id)
        {
            var retenue = await _retenueService.GetRetenueByIdAsync(id);
            if (retenue == null)
            {
                return NotFound("Retenue introuvable.");
            }

            await _retenueService.DeleteRetenueAsync(retenue);
            return NoContent(); // Indique que la suppression a réussi
        }

        // Obtenir une retenue par ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRetenueByIdAsync(int id)
        {
            var retenue = await _retenueService.GetRetenueByIdAsync(id);
            if (retenue == null)
            {
                return NotFound("Retenue introuvable.");
            }

            var retenueDto = new RetenueDto
            {
                ID_Retenue = retenue.ID_Retenue,
                Taux = retenue.Taux,
                Categorie = retenue.Categorie,
                ID_FactureAchat = retenue.ID_FactureAchat
            };

            return Ok(retenueDto);
        }

        // Obtenir les retenues par ID de fournisseur
        [HttpGet("fournisseur/{fournisseurId}")]
        public async Task<IActionResult> GetRetenuesByFournisseurIdAsync(int fournisseurId)
        {
            var retenues = await _retenueService.GetRetenuesByFournisseurIdAsync(fournisseurId);
            if (retenues == null || !retenues.Any())
            {
                return NotFound("Aucune retenue trouvée pour ce fournisseur.");
            }

            var retenueDtos = retenues.Select(retenue => new RetenueDto
            {
                ID_Retenue = retenue.ID_Retenue,
                Taux = retenue.Taux,
                Categorie = retenue.Categorie,
                ID_FactureAchat = retenue.ID_FactureAchat
            });

            return Ok(retenueDtos);
        }
    }
}
