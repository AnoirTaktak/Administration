﻿using Administration.Dtos;
using Administration.Models;
using Administration.Services.Utilisateur;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Administration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilisateurController : ControllerBase
    {
        private readonly IUtilisateur_Service _utilisateurService;
        private readonly IConfiguration _configuration;

        public UtilisateurController(IUtilisateur_Service utilisateurService, IConfiguration configuration)
        {
            _utilisateurService = utilisateurService;
            _configuration = configuration;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAllUtilisateursAsync()
        {
            var utilisateurs = await _utilisateurService.GetAllUtilisateurs();
            return Ok(utilisateurs);
        }

       

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUtilisateurByIdAsync(int id)
        {
            var utilisateur = await _utilisateurService.GetUtilisateurById(id);
            return Ok(utilisateur);
        }

    

        [HttpPost]
        public async Task<IActionResult> CreateUtilisateurAsync(UtilisateurDto utilisateurDto)
        {
            var utilisateur = new Utilisateur
            {
                Nom_Utilisateur = utilisateurDto.Nom_Utilisateur,
                Pseudo = utilisateurDto.Pseudo,
                Email_Utilisateur = utilisateurDto.Email_Utilisateur,
                MotDePasse_Utilisateur = utilisateurDto.MotDePasse_Utilisateur,
                Role_Utilisateur = utilisateurDto.Role_Utilisateur
            };

             var rep = await _utilisateurService.AddUtilisateur(utilisateur);
            if(rep!=null) return Ok();
            return Ok();
        }

      
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUtilisateurAsync(int id, UtilisateurDto utilisateurDto)
        {
            var utilisateur = await _utilisateurService.GetUtilisateurById(id);
            if (utilisateur == null)
            {
                return NotFound("Utilisateur introuvable.");
            }

            utilisateur.Nom_Utilisateur = utilisateurDto.Nom_Utilisateur;
            utilisateur.Pseudo = utilisateurDto.Pseudo;
            utilisateur.Email_Utilisateur = utilisateurDto.Email_Utilisateur;
            utilisateur.MotDePasse_Utilisateur = utilisateurDto.MotDePasse_Utilisateur;
            utilisateur.Role_Utilisateur = utilisateurDto.Role_Utilisateur;

            await _utilisateurService.UpdateUtilisateur(utilisateur);
            return Ok();
        }

    
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtilisateurAsync(int id)
        {
            var utilisateur = await _utilisateurService.GetUtilisateurById(id);
            if (utilisateur == null)
            {
                return NotFound("Utilisateur introuvable.");
            }

            await _utilisateurService.DeleteUtilisateur(utilisateur);
            return Ok();

        }


        // Méthode pour authentifier l'utilisateur et générer un token JWT
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(UtilisateurDto utilisateurDto)
        {
            var utilisateur = await _utilisateurService.GetUtilisateurByUsername(utilisateurDto.Nom_Utilisateur);
            if (utilisateur == null || utilisateur.MotDePasse_Utilisateur != utilisateurDto.MotDePasse_Utilisateur)
            {
                return NotFound("Nom d'utilisateur ou mot de passe incorrect.");
            }

            // Création du token JWT
            var token = GenerateJwtToken(utilisateur);
            return Ok(new { Token = token });
        }

        // Méthode privée pour générer le JWT
        private string GenerateJwtToken(Utilisateur utilisateur)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, utilisateur.Nom_Utilisateur),
                new Claim(JwtRegisteredClaimNames.Email, utilisateur.Email_Utilisateur),
                new Claim(ClaimTypes.Role, utilisateur.Role_Utilisateur.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        
    }
}
