using Administration.Dtos;
using DocumentModel = Administration.Models.Document; // Création de l'alias

namespace Administration.Services.Document
{
    public interface IDocument_Service
    {
        Task<string> GenerateDocumentContent(DocumentDto documentDto);
        Task<DocumentModel> CreateDocumentAsync(DocumentModel documentDto);
        Task<DocumentModel> UpdateDocumentAsync(DocumentModel documentDto);
        Task DeleteDocumentAsync(int id);
    }
}
