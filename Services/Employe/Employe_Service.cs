using Administration.Models;
using Microsoft.EntityFrameworkCore;
using EmployeModel = Administration.Models.Employe; // Création de l'alias


namespace Administration.Services.Employe
{
    public class Employe_Service : IEmploye_Service
    {
        private readonly AppDBContext _context;

        public Employe_Service(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeModel>> GetAllEmployes()
        {
            return await _context.Employes.ToListAsync();
        }

        public async Task<EmployeModel> GetEmployeById(int id)
        {
            var employe = await _context.Employes.SingleOrDefaultAsync(e => e.ID_Employe == id);

            if (employe == null)
            {
                throw new ArgumentException($"Employe avec ID : '{id}' n'existe pas.");
            }

            return employe;
        }

        public async Task<string> AddEmploye(EmployeModel employe)
        {
            // Vérifier si le nom existe déjà
            if (_context.Employes.Any(e => e.Nom_Employe == employe.Nom_Employe))
            {
                return "Erreur: Le nom employe est déja utilisé '" + employe.Nom_Employe + "'.";
            }

            // Vérifier si CIN existe déjà
            if (_context.Employes.Any(e => e.CIN_Employe == employe.CIN_Employe))
            {
                return "Erreur: Le numéro de CIN est déja utilisé '" + employe.CIN_Employe + "'.";
            }

            // Vérifier si Cnss existe déjà
            if (_context.Employes.Any(e => e.CNSS_Employe == employe.CNSS_Employe))
            {
                return "Erreur: Le numéro de Cnss est déja utilisé '" + employe.CIN_Employe + "'.";
            }
            await _context.AddAsync(employe);
            _context.SaveChanges();
            return "Employe ajouté avec succès.";
        }

        public string UpdateEmploye(EmployeModel employe)
        {
            // Vérifier si le nom existe déjà
            if (_context.Employes.Any(e => e.Nom_Employe == employe.Nom_Employe && e.ID_Employe != employe.ID_Employe))
            {
                return "Erreur: Le nom employe est déja utilisé '" + employe.Nom_Employe + "'.";
            }

            // Vérifier si CIN existe déjà
            if (_context.Employes.Any(e => e.CIN_Employe == employe.CIN_Employe && e.ID_Employe != employe.ID_Employe))
            {
                return "Erreur: Le numéro de CIN est déja utilisé '" + employe.CIN_Employe + "'.";
            }

            // Vérifier si Cnss existe déjà
            if (_context.Employes.Any(e => e.CNSS_Employe == employe.CNSS_Employe && e.ID_Employe != employe.ID_Employe))
            {
                return "Erreur: Le numéro de Cnss est déja utilisé '" + employe.CIN_Employe + "'.";
            }
            _context.Update(employe);
            _context.SaveChanges();
            return "Employe modifié avec succès.";
        }

        public EmployeModel DeleteEmploye(EmployeModel employe)
        {
            _context.Remove(employe);
            _context.SaveChanges();
            return employe;
        }

        public async Task<IEnumerable<EmployeModel>> GetEmployeByNom(string nom)
        {
            var employes = await _context.Employes.Where(e => e.Nom_Employe.Contains(nom)).ToListAsync();

            return employes;
        }

        public async Task<IEnumerable<EmployeModel>> GetEmployesByTypeContrat(TypeContrat typeContrat)
        {
            var employes = await _context.Employes.Where(e => e.TypeContrat == typeContrat).ToListAsync();
           
            return employes;
        }

        public async Task<IEnumerable<EmployeModel>> GetEmployeByCin(string cin)
        {
            var employes = await _context.Employes.Where(e => e.CIN_Employe.Contains(cin)).ToListAsync();

            return employes;
        }
    }
}
