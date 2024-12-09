using Administration.Dtos;
using Administration.Models;
using Administration.Services.Document;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using DocumentModel = Administration.Models.Document; // Création de l'alias


namespace Administration.Services
{
    public class Document_Service : IDocument_Service
    {
        private readonly AppDBContext _Context;

        public Document_Service(AppDBContext dbContext)
        {
            _Context = dbContext;
        }

        public async Task<string> GenerateDocumentContent(DocumentDto documentDto)
        {
            var typeDocument = await _Context.TypesDocuments.FindAsync(documentDto.ID_TypeDocument);
            var societe = await _Context.Societes.FindAsync(documentDto.ID_Societe);
            var employe = await _Context.Employes.FindAsync(documentDto.ID_Employe);
   

            // Générer le contenu
            var contenu = typeDocument.Template
                .Replace("{RaisonSocial_Societe}", societe.RaisonSociale_Societe)
                .Replace("{Poste}", employe.Poste_Employe)
                .Replace("{Adresse}", societe.Adresse_Societe)
                .Replace("{Tel}", societe.Tel_Societe)
                .Replace("{Email_Societe}", societe.Email_Societe)
                .Replace("{CodePostal}", societe.CodePostal.ToString())
                .Replace("{Nom_Societe}", societe.RaisonSociale_Societe)
                .Replace("{Matricule_Fiscal}", societe.MF_Societe)
                .Replace("{CIN_Employe}", employe.CIN_Employe)
                .Replace("{CNSS_Employe}", employe.CNSS_Employe)
                .Replace("{DateDebut}", employe.DateDebut.ToString())
                .Replace("{DateFin}", employe.DateFin.ToString())
                .Replace("{TypeContrat}", employe.TypeContrat.ToString())
                .Replace("{Nom_Employe}", employe.Nom_Employe)
                .Replace("{Date}", DateTime.Now.ToString("dd/MM/yyyy"));

            return contenu;
        }


        public async Task<DocumentModel> CreateDocumentAsync(DocumentModel documentDto)
        {
            // Charger le type de document
            var typeDocument = await _Context.TypesDocuments.FindAsync(documentDto.ID_TypeDocument);
            if (typeDocument == null) throw new Exception("Type de document introuvable");

            // Charger les données de l'employé (simulé ici)
            var employe = await _Context.Employes.FindAsync(documentDto.ID_Employe);
            if (employe == null) throw new Exception("Employé introuvable");

            // Charger les données de l'employé (simulé ici)
            var societe = await _Context.Societes.FindAsync(documentDto.ID_Societe);
            if (employe == null) throw new Exception("Societe introuvable");


            // Générer le contenu
            var contenu = typeDocument.Template
                .Replace("{RaisonSocial_Societe}", societe.RaisonSociale_Societe)
                .Replace("{Poste}.",employe.Poste_Employe)
                .Replace("{Adresse}", societe.Adresse_Societe)
                .Replace("{Tel}", societe.Tel_Societe)
                .Replace("{Email_Societe}", societe.Email_Societe)
                .Replace("{CodePostal}", societe.CodePostal.ToString())
                .Replace("{Nom_Societe}", societe.RaisonSociale_Societe)
                .Replace("{Matricule_Fiscal}", societe.MF_Societe)
                .Replace("{CIN_Employe},", employe.CIN_Employe)
                .Replace("{CNSS_Employe}", employe.CNSS_Employe)
                .Replace("{DateDebut}", employe.DateDebut.ToString())
                .Replace("{DateFin}", employe.DateFin.ToString())
                .Replace("{TypeContrat}", employe.TypeContrat.ToString())
                .Replace("{Nom_Employe}", employe.Nom_Employe)
                .Replace("{Date}", DateTime.Now.ToString("dd/MM/yyyy"));

            // Créer le document
            var document = new DocumentModel
            {
                ID_Employe = documentDto.ID_Employe,
                ID_TypeDocument = documentDto.ID_TypeDocument,
                Date = DateTime.Now,
                Contenu = contenu,
                Doc_Pdf = documentDto.Doc_Pdf
            };

            _Context.Documents.Add(document);
             await _Context.SaveChangesAsync();

            return new DocumentModel
            {
                ID_Employe = document.ID_Employe,
                ID_TypeDocument = document.ID_TypeDocument,
                ID_Societe = document.ID_Societe,
                Date = document.Date,
                Contenu = document.Contenu,
                Doc_Pdf = documentDto.Doc_Pdf
            };
        }


        

       

        public async Task<DocumentModel> UpdateDocumentAsync(DocumentModel documentDto)
        {
            var document = await _Context.Documents.FindAsync(documentDto);
            if (document == null) return null;

            document.Contenu = documentDto.Contenu;
            // Mettre à jour d'autres champs si nécessaire

            await _Context.SaveChangesAsync();

            return documentDto;
        }

        public async Task DeleteDocumentAsync(int id)
        {
            var document = await _Context.Documents.FindAsync(id);
            if (document != null)
            {
                _Context.Documents.Remove(document);
                await _Context.SaveChangesAsync();
            }
        }
    }
}
