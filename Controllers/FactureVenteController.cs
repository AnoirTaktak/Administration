using Administration.Dtos;
using Administration.Models;
using Administration.Services.FactureVente;
using Microsoft.AspNetCore.Mvc;

namespace Administration.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FactureVenteController : ControllerBase
    {
        private readonly IFactureVente_Service _factureVenteService;
        private readonly AppDBContext _context;

        public FactureVenteController(IFactureVente_Service factureVenteService, AppDBContext context)
        {
            _factureVenteService = factureVenteService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFactures()
        {
            var factures = await _factureVenteService.GetAllFactures();
            return Ok(factures);
        }

        [HttpGet("numero/{numeroFacture}")]
        public async Task<IActionResult> GetFactureByNumero(string numeroFacture)
        {
            var facture = await _factureVenteService.GetFactureByNumero(numeroFacture);
            if (facture == null)
            {
                return NotFound();
            }
            return Ok(facture);
        }

        [HttpGet("client/{clientId}")]
        public async Task<IActionResult> GetFacturesByClient(int clientId)
        {
            var factures = await _factureVenteService.GetFacturesByClient(clientId);
            return Ok(factures);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFacture([FromBody] FactureVenteDto factureVenteDto)
        {
            if (factureVenteDto == null)
            {
                return BadRequest("Facture data is null.");
            }
            var facture = await _factureVenteService.CreateFacture(factureVenteDto);
            return CreatedAtAction(nameof(GetFactureByNumero), new { numeroFacture = facture.NumeroFacture }, facture);
        }

        [HttpGet("income-stats")]
        public IActionResult GetIncomeStats()
        {
            var currentMonthTotal = _context.FacturesVente
                .Where(f => f.DateFacture.Month == DateTime.Now.Month && f.DateFacture.Year == DateTime.Now.Year)
                .Sum(f => f.Total_FactureVente);

            var lastMonthTotal = _context.FacturesVente
                .Where(f => f.DateFacture.Month == DateTime.Now.AddMonths(-1).Month && f.DateFacture.Year == DateTime.Now.Year)
                .Sum(f => f.Total_FactureVente);

            var progressPercentage = lastMonthTotal > 0
                ? ((currentMonthTotal - lastMonthTotal) / lastMonthTotal) * 100
                : 100;

            return Ok(new
            {
                currentMonthTotal,
                progressPercentage
            });
        }

    }
}
