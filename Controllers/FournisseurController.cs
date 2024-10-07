using Administration.Dtos;
using Administration.Models;
using Administration.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Administration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FournisseurController : ControllerBase
    {
        private readonly IFournisseur_Service _fournisseur_Service;

        public FournisseurController(IFournisseur_Service fournisseur_Service)
        {
            _fournisseur_Service = fournisseur_Service;
        }
        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpGet]
        public async Task<IActionResult> GetAllFournisseursAsync()
        {
            var fournisseurs = await _fournisseur_Service.GetAllFournisseurs();
            return Ok(fournisseurs);
        }
        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFournisseurByIdAsync(int id)
        {
            var fournisseur = await _fournisseur_Service.GetFournisseurById(id);
            if (fournisseur == null)
            {
                return NotFound("Fournisseur introuvable");
            }
            return Ok(fournisseur);
        }
        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpPost]
        public async Task<IActionResult> CreateFournisseurAsync(Fournisseur fournisseur)
        {
            var frn = new Fournisseur
            {
                Adresse_Fournisseur = fournisseur.Adresse_Fournisseur,
                Email_Fournisseur = fournisseur.Email_Fournisseur,
                MF_Fournisseur = fournisseur.MF_Fournisseur,
                RaisonSociale_Fournisseur = fournisseur.RaisonSociale_Fournisseur,
                Tel_Fournisseur = fournisseur.Tel_Fournisseur,
                Type_Fournisseur = fournisseur.Type_Fournisseur
            };
            var createdFournisseur = await _fournisseur_Service.AddFournisseur(frn);
            return Ok(fournisseur);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFournisseurAsync(int id, FournisseurDto fournisseur)
        {
            var existingFournisseur = await _fournisseur_Service.GetFournisseurById(id);
            if (existingFournisseur == null)
            {
                return NotFound("Fournisseur introuvable");
            }

            existingFournisseur.MF_Fournisseur = fournisseur.MF_Fournisseur;
            existingFournisseur.Adresse_Fournisseur = fournisseur.Adresse_Fournisseur;
            existingFournisseur.Email_Fournisseur = fournisseur.Email_Fournisseur;
            existingFournisseur.Tel_Fournisseur = fournisseur.Tel_Fournisseur;
            existingFournisseur.RaisonSociale_Fournisseur = fournisseur.RaisonSociale_Fournisseur;
            existingFournisseur.Type_Fournisseur = fournisseur.Type_Fournisseur;
            
            _fournisseur_Service.UpdateFournisseur(existingFournisseur);
            return Ok(existingFournisseur);
        }


        [Authorize(Roles = "SuperAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFournisseurAsync(int id)
        {
            var fournisseur = await _fournisseur_Service.GetFournisseurById(id);
            if (fournisseur == null)
            {
                return NotFound("Fournisseur introuvable pour suppression");
            }
            _fournisseur_Service.DeleteFournisseur(fournisseur);
            return NoContent(); // 204 No Content
        }

        [HttpGet("rs/{rs}")]
        public async Task<IActionResult> GetFournisseurByRSAsync(string rs)
        {
            var fournisseur = await _fournisseur_Service.GetFournisseurByRS(rs);
            if (fournisseur == null)
            {
                return NotFound("Fournisseur introuvable");
            }
            return Ok(fournisseur);
        }
    }
}
