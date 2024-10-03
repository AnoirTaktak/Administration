using Administration.Dtos;
using Administration.Models;
using Administration.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Administration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocieteController : ControllerBase
    {
        private readonly Societe_Service _societe_Service;

        public SocieteController(Societe_Service societe_Service)
        {
            _societe_Service = societe_Service;
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        public async Task<IActionResult> GetAllSocietesAsync()
        {
            var societes = await _societe_Service.GetAllSocietes();
            return Ok(societes);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSocieteByIdAsync(int id)
        {
            var societe = await _societe_Service.GetSocieteById(id);
            if (societe == null)
            {
                return NotFound("Société introuvable.");
            }
            return Ok(societe);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        public async Task<IActionResult> CreateSocieteAsync(SocieteDto societeDto)
        {
            var societe = new Societe
            {
                RaisonSociale_Societe = societeDto.RaisonSociale_Societe,
                MF_Societe = societeDto.MF_Societe,
                Adresse_Societe = societeDto.Adresse_Societe,
                Tel_Societe = societeDto.Tel_Societe,
                CodePostal = societeDto.CodePostal,
                Cachet = societeDto.Cachet,
                Signature = societeDto.Signature
            };

            await _societe_Service.AddSociete(societe);
            return Ok(societe);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSocieteAsync(int id, SocieteDto societeDto)
        {
            var societe = await _societe_Service.GetSocieteById(id);
            if (societe == null)
            {
                return NotFound("Société introuvable.");
            }

            societe.MF_Societe = societeDto.MF_Societe;
            societe.RaisonSociale_Societe = societeDto.RaisonSociale_Societe;
            societe.Adresse_Societe = societeDto.Adresse_Societe;
            societe.Tel_Societe = societeDto.Tel_Societe;
            societe.CodePostal = societeDto.CodePostal;
            societe.Cachet = societeDto.Cachet;
            societe.Signature = societeDto.Signature;

            _societe_Service.UpdateSociete(societe);
            return Ok(societe);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSocieteAsync(int id)
        {
            var societe = await _societe_Service.GetSocieteById(id);
            if (societe == null)
            {
                return NotFound("Société introuvable.");
            }
            _societe_Service.DeleteSociete(societe);
            return NoContent(); // Indique que la suppression a réussi
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet("{id}/cachet")]
        public async Task<IActionResult> GetCachetAsync(int id)
        {
            var societe = await _societe_Service.GetSocieteById(id);
            if (societe == null || societe.Cachet == null)
            {
                return NotFound("Cachet introuvable.");
            }
            return File(societe.Cachet, "image/png"); // Remplacez "image/png" par le type MIME approprié
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet("{id}/signature")]
        public async Task<IActionResult> GetSignatureAsync(int id)
        {
            var societe = await _societe_Service.GetSocieteById(id);
            if (societe == null || societe.Signature == null)
            {
                return NotFound("Signature introuvable.");
            }
            return File(societe.Signature, "image/png"); // Remplacez "image/png" par le type MIME approprié
        }
    }
}
