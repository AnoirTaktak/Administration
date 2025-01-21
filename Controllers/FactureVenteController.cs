using Administration.Dtos;
using Administration.Models;
using Administration.Services.FactureVente;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet("total-ventes-last-five-months")]
        public async Task<IActionResult> GetTotalVentesLastFiveMonths()
        {
            try
            {
                var result = await _context.FacturesVente
                    .Where(f => f.DateFacture >= DateTime.Now.AddMonths(-5)) // Filtrer les 5 derniers mois
                    .GroupBy(f => new { f.DateFacture.Year, f.DateFacture.Month }) // Grouper par année et mois
                    .Select(g => new
                    {
                        Month = g.Key.Month,
                        Year = g.Key.Year,
                        Total = g.Sum(f => f.Total_FactureVente) // Totaliser les factures par groupe
                    })
                    .OrderBy(g => g.Year).ThenBy(g => g.Month) // Trier par année puis mois
                    .ToListAsync();

                // Construire un tableau des totaux dans l'ordre chronologique
                var totals = result
                    .Select(r => r.Total)
                    .ToList();

                // Remplir les mois manquants avec des totaux de 0
                for (int i = 5; i > result.Count; i--)
                {
                    totals.Insert(0, 0);
                }

                return Ok(totals);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Erreur lors de la récupération des totaux de vente.", Details = ex.Message });
            }
        }

    }
}
