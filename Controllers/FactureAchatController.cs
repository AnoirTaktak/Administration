using Administration.Dtos;
using Administration.Models;
using Administration.Services.FactureAchat; // Assurez-vous d'importer le bon namespace
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Administration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FactureAchatController : ControllerBase
    {
        private readonly IFactureAchat_Service _factureAchatService; // Interface de service
        private readonly IMapper _mapper;

        private readonly List<string> _allowedExtensions = new List<string> { ".jpg", ".png", ".jpeg", ".pdf" };

        public FactureAchatController(IFactureAchat_Service factureAchatService, IMapper mapper)
        {
            _factureAchatService = factureAchatService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFacturesAsync()
        {
            var factures = await _factureAchatService.GetAllFacturesAchat();
            var data = _mapper.Map<IEnumerable<FactureAchat>>(factures);
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFactureByIdAsync(int id)
        {
            var facture = await _factureAchatService.GetFactureAchatById(id);
            if (facture == null)
            {
                return NotFound("Facture introuvable.");
            }
            var data = _mapper.Map<FactureAchat>(facture);
            return Ok(data);
        }

        [HttpGet("byfournisseur/{idFournisseur}")]
        public async Task<IActionResult> GetFacturesByFournisseurAsync(int idFournisseur)
        {
            var factures = await _factureAchatService.GetFacturesAchatByFournisseur(idFournisseur);
            if (factures == null || !factures.Any())
            {
                return NotFound("Aucune facture d'achat trouvée pour ce fournisseur.");
            }

            var data = _mapper.Map<IEnumerable<FactureAchat>>(factures);
            return Ok(data);
        }

        [HttpGet("by-etat/{etat}")]
        public async Task<IActionResult> GetFacturesByEtatAsync(bool etat)
        {
            var factures = await _factureAchatService.GetFacturesAchatByEtat(etat);
            if (factures == null || !factures.Any())
            {
                return NotFound("Aucune facture d'achat trouvée pour cet état.");
            }

            var data = _mapper.Map<IEnumerable<FactureAchat>>(factures);
            return Ok(data);
        }

        [HttpGet("ByDateRange")]
        public async Task<IActionResult> GetFacturesByDateRange([FromQuery] DateOnly? startDate, [FromQuery] DateOnly? endDate)
        {
            if (!startDate.HasValue && !endDate.HasValue)
                return BadRequest("Veuillez fournir au moins une date de début ou une date de fin.");

            var factures = await _factureAchatService.GetFacturesByDateRangeAsync(startDate, endDate);
            return Ok(factures);
        }


        [HttpGet("bynumfac")]
        public async Task<IActionResult> GetFacturesByNumFacAsync(string nf)
        {
            var factures = await _factureAchatService.GetFacturesAchatByNumFac(nf);
            return Ok(factures);
        }


        [HttpPost]
        public async Task<IActionResult> CreateFactureAchatAsync([FromForm] FactureAchatDto factureAchatDto)
        {
            if (factureAchatDto == null)
            {
                return BadRequest("Aucune donnée fournie.");
            }

            byte[]? imageData = null;

            if (factureAchatDto.ImageFacture != null)
            {
                var extension = Path.GetExtension(factureAchatDto.ImageFacture.FileName).ToLower();
                if (!_allowedExtensions.Contains(extension))
                {
                    return BadRequest("Seules les extensions jpg, png, jpeg, et pdf sont autorisées.");
                }

                using var dataStream = new MemoryStream();
                await factureAchatDto.ImageFacture.CopyToAsync(dataStream);
                imageData = dataStream.ToArray(); // Conversion en tableau d'octets
                // Vous pouvez directement assigner imageData à l'objet mappé après
            }

            var factureAchat = _mapper.Map<FactureAchat>(factureAchatDto);
            factureAchat.ImageFacture = imageData; // Assurez-vous de l'assigner ici

            var result = await _factureAchatService.AddFactureAchat(factureAchat);
            if (result.StartsWith("Erreur"))
            {
                return BadRequest(result);
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFactureAchatAsync(int id, [FromForm] FactureAchatDto factureAchatDto)
        {
            var factureAchat = await _factureAchatService.GetFactureAchatById(id);
            if (factureAchat == null)
            {
                return NotFound("Facture introuvable.");
            }

            if (factureAchatDto.ImageFacture != null)
            {
                var extension = Path.GetExtension(factureAchatDto.ImageFacture.FileName).ToLower();
                if (!_allowedExtensions.Contains(extension))
                {
                    return BadRequest("Seules les extensions jpg, png, jpeg, et pdf sont autorisées.");
                }

                using var dataStream = new MemoryStream();
                await factureAchatDto.ImageFacture.CopyToAsync(dataStream);
                factureAchat.ImageFacture = dataStream.ToArray(); // Mise à jour de l'image
            }

            factureAchat.DateAchat = factureAchatDto.DateAchat;
            factureAchat.Montant = factureAchatDto.Montant;
            factureAchat.EtatPaiement = factureAchatDto.EtatPaiement;
            factureAchat.ID_Fournisseur = factureAchatDto.ID_Fournisseur;

            await _factureAchatService.UpdateFactureAchat(factureAchat);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFactureAchatAsync(int id)
        {
            var facture = await _factureAchatService.GetFactureAchatById(id);
            if (facture == null)
            {
                return NotFound("Facture introuvable.");
            }

            await _factureAchatService.DeleteFactureAchat(facture);
            return Ok();
        }
    }
}
