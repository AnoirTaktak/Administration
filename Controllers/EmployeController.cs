using Administration.Dtos;
using Administration.Models;
using Administration.Services.Employe;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Administration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeController : ControllerBase
    {
        private readonly IEmploye_Service _employe_Service;

        public EmployeController(IEmploye_Service employe_Service)
        {
            _employe_Service = employe_Service;
        }
        

        [HttpGet]
        public async Task<IActionResult> GetAllEmployesAsync()
        {
            var employes = await _employe_Service.GetAllEmployes();
            return Ok(employes);
        }
        

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeByIdAsync(int id)
        {
            var employe = await _employe_Service.GetEmployeById(id);
            if (employe == null)
            {
                return NotFound("Employé introuvable");
            }
            return Ok(employe);
        }


        [HttpGet("nom/{nom}")]
        public async Task<IActionResult> GetEmployeByNomAsync(string nom)
        {
            var employes = await _employe_Service.GetEmployeByNom(nom);
            if (employes == null)
            {
                return NotFound("Employé introuvable");
            }
            return Ok(employes);
        }

        [HttpGet("cin/{cin}")]
        public async Task<IActionResult> GetEmployeByCinAsync(string cin)
        {
            var employes = await _employe_Service.GetEmployeByCin(cin);
            if (employes == null)
            {
                return NotFound("Client introuvable");
            }
            return Ok(employes);
        }


        [HttpGet("typecontrat/{tc}")]
        public async Task<IActionResult> GetEmployeByTCAsync(TypeContrat tc)
        {
            var employes = await _employe_Service.GetEmployesByTypeContrat(tc);
            if (employes == null)
            {
                return NotFound("Employe introuvable");
            }
            return Ok(employes);
        }



        [HttpPost]
        public async Task<IActionResult> CreateEmployeAsync(Employe employeDto)
        {
            var employe = new Employe
            {
                Nom_Employe = employeDto.Nom_Employe,
                Email_Employe = employeDto.Email_Employe,
                TypeContrat = employeDto.TypeContrat,
                DateDebut = employeDto.DateDebut,
                DateFin = employeDto.DateFin,
                Salaire = employeDto.Salaire,
                CIN_Employe = employeDto.CIN_Employe,
                CNSS_Employe = employeDto.CNSS_Employe,
                Poste_Employe = employeDto.Poste_Employe,
                Tel_Employe = employeDto.Tel_Employe,
            };

            var result =  await _employe_Service.AddEmploye(employe);

            // Si un message d'erreur est retourné, renvoyer un code 400 avec le message d'erreur
            if (result.StartsWith("Erreur"))
            {
                return BadRequest(result);
            }

            return Ok();
        }
        

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployeAsync(int id, EmployeDto employeDto)
        {
            var employe = await _employe_Service.GetEmployeById(id);
            if (employe == null)
            {
                return NotFound("Employé introuvable");
            }

            employe.Nom_Employe = employeDto.Nom_Employe;
            employe.Email_Employe = employeDto.Email_Employe;
            employe.TypeContrat = employeDto.TypeContrat;
            employe.DateDebut = employeDto.DateDebut;
            employe.DateFin = employeDto.DateFin;
            employe.Salaire = employeDto.Salaire;
            employe.CIN_Employe = employeDto.CIN_Employe;
            employe.CNSS_Employe = employeDto.CNSS_Employe;
            employe.Poste_Employe = employeDto.Poste_Employe;
            employe.Tel_Employe = employeDto.Tel_Employe;
            
            await _employe_Service.UpdateEmploye(employe);

            return Ok();
        }
        

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeAsync(int id)
        {
            var employe = await _employe_Service.GetEmployeById(id);
            if (employe == null)
            {
                return NotFound("Employé introuvable pour suppression");
            }
             await _employe_Service.DeleteEmploye(employe);
            
            return Ok();
        }
    }
}
