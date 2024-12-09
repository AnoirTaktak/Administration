using Administration.Dtos;
using Administration.Migrations;
using Administration.Models;
using Administration.Services.Societe;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Administration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocieteController : ControllerBase
    {
        private readonly ISociete_Service _societe_Service;
        private readonly IMapper _mapper;

        private new List<string> _allowedExtensions = new List<string> { ".jpg", ".png", ".jpeg", ".pdf" };

        public SocieteController(ISociete_Service societe_Service,IMapper mapper)
        {
            _societe_Service = societe_Service;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var societes = await _societe_Service.GetAllSocietes();
                var societesDto = _mapper.Map<IEnumerable<Societe>>(societes);
                return Ok(societesDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur : {ex.Message}");
            }
        }




        [HttpGet("/rs/{rs}")]
        public async Task<IActionResult> GetSocieteByRSAsync(string rs)
        {
            var societe = await _societe_Service.GetSocieteByRS(rs);
            if (societe == null)
            {
                return NotFound("Société introuvable.");
            }
            var data = _mapper.Map<List<Societe>>(societe);
            return Ok(data);
        }

        [HttpGet("/mf/{mf}")]
        public async Task<IActionResult> GetSocieteByMFAsync(string mf)
        {
            var societe = await _societe_Service.GetSocieteByMF(mf);
            if (societe == null)
            {
                return NotFound("Société introuvable.");
            }
            var data = _mapper.Map<List<SocieteDto>>(societe);
            return Ok(data);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetSocieteByIdAsync(int id)
        {
            var societe = await _societe_Service.GetSocieteById(id);
            if (societe == null)
            {
                return NotFound("Société introuvable.");
            }
            var data = _mapper.Map<Societe>(societe);
            return Ok(data);
        }



        [HttpPost]
        public async Task<IActionResult> CreateSocieteAsync(SocieteDto societeDto)
        {
            if (societeDto == null)
            {
                return BadRequest("no data");
            }
            if (societeDto.CachetSignature == null)
            {
                return BadRequest("Le fichier de cachet/signature est requis.");
            }


           
             if (!string.IsNullOrEmpty(societeDto.CachetSignature?.FileName) &&
                    !_allowedExtensions.Contains(Path.GetExtension(societeDto.CachetSignature.FileName).ToLower()))
                {
                    return BadRequest("seulement jpg or png");
                }

                using var datastream = new MemoryStream();
                await societeDto.CachetSignature.CopyToAsync(datastream);
                var societe1 = _mapper.Map<Societe>(societeDto);
                societe1.CachetSignature = datastream.ToArray();
                await _societe_Service.AddSociete(societe1);
                return Ok($"La societe {societe1.RaisonSociale_Societe} a été ajouté ");
          
           
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSocieteAsync(int id, SocieteDto societeDto)
        {
            var societe = await _societe_Service.GetSocieteById(id);
            if (societeDto == null)
            {
                return BadRequest("no data id");
            }

            if (societeDto.CachetSignature != null)
            {
                if (!string.IsNullOrEmpty(societeDto.CachetSignature?.FileName) &&
                    !_allowedExtensions.Contains(Path.GetExtension(societeDto.CachetSignature.FileName).ToLower()))
                {
                    return BadRequest("seulement jpg ou png");
                }

                using var datastream = new MemoryStream();
                await societeDto.CachetSignature.CopyToAsync(datastream);
                societe.CachetSignature = datastream.ToArray();
            }

            if (societe == null)
            {
                return NotFound("Société introuvable.");
            }

            // Met à jour les autres propriétés
            societe.MF_Societe = societeDto.MF_Societe;
            societe.RaisonSociale_Societe = societeDto.RaisonSociale_Societe;
            societe.Adresse_Societe = societeDto.Adresse_Societe;
            societe.Tel_Societe = societeDto.Tel_Societe;
            societe.CodePostal = societeDto.CodePostal;

            await _societe_Service.UpdateSociete(societe);
            return Ok($"La societe {societe.RaisonSociale_Societe} a été modifiée ");
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSocieteAsync(int id)
        {
            var societe = await _societe_Service.GetSocieteById(id);
            if (societe == null)
            {
                return NotFound("Société introuvable.");
            }
            await _societe_Service.DeleteSociete(societe);
            return Ok($"La societe {societe.RaisonSociale_Societe} a été supprimé "); // Indique que la suppression a réussi
        }

       
    }
}
