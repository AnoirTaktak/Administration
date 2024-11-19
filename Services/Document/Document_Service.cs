using Administration.Dtos;
using Administration.Models;
using Administration.Services.Document;
using Microsoft.EntityFrameworkCore;
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

        public async Task<DocumentDto> CreateDocumentAsync(DocumentDto documentDto)
        {
            var document = new DocumentModel
            {
                ID_Employe = documentDto.ID_Employe,
                ID_TypeDocument = documentDto.ID_TypeDocument,
                Date = documentDto.Date,
                Contenu = documentDto.Contenu
            };

            _Context.Documents.Add(document);
            await _Context.SaveChangesAsync();

            return documentDto;
        }

        public async Task<DocumentDto> GetDocumentByIdAsync(int id)
        {
            var document = await _Context.Documents.FindAsync(id);
            if (document == null) return null;

            return new DocumentDto
            {
                ID_Employe = document.ID_Employe,
                ID_TypeDocument = document.ID_TypeDocument,
                Date = document.Date,
                Contenu = document.Contenu
            };
        }

        public async Task<IEnumerable<DocumentDto>> GetDocumentsByEmployeeIdAsync(int employeeId)
        {
            var documents = await _Context.Documents
                .Where(d => d.ID_Employe == employeeId)
                .ToListAsync();

            return documents.Select(d => new DocumentDto
            {
                ID_Employe = d.ID_Employe,
                ID_TypeDocument = d.ID_TypeDocument,
                Date = d.Date,
                Contenu = d.Contenu
            });
        }

        public async Task<DocumentDto> UpdateDocumentAsync(DocumentDto documentDto)
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
