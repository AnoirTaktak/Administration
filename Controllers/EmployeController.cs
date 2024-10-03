using Administration.Dtos;
using Administration.Models;
using Administration.Services;
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
        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpGet]
        public async Task<IActionResult> GetAllEmployesAsync()
        {
            var employes = await _employe_Service.GetAllEmployes();
            return Ok(employes);
        }
        [Authorize(Roles = "Admin, SuperAdmin")]
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
        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpPost]
        public async Task<IActionResult> CreateEmployeAsync(EmployeDto employeDto)
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
            };

            await _employe_Service.AddEmploye(employe);
            return CreatedAtAction(nameof(GetEmployeByIdAsync), new { id = employe.ID_Employe }, employe);
        }
        [Authorize(Roles = "Admin, SuperAdmin")]
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

            _employe_Service.UpdateEmploye(employe);
            return Ok(employe);
        }
        [Authorize(Roles = "SuperAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeAsync(int id)
        {
            var employe = await _employe_Service.GetEmployeById(id);
            if (employe == null)
            {
                return NotFound("Employé introuvable pour suppression");
            }
            _employe_Service.DeleteEmploye(employe);
            return NoContent(); // 204 No Content
        }
    }
}
