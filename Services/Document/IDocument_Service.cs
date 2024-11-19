using Administration.Dtos;

namespace Administration.Services.Document
{
    public interface IDocument_Service
    {
        Task<DocumentDto> CreateDocumentAsync(DocumentDto documentDto);
        Task<DocumentDto> GetDocumentByIdAsync(int id);
        Task<IEnumerable<DocumentDto>> GetDocumentsByEmployeeIdAsync(int employeeId);
        Task<DocumentDto> UpdateDocumentAsync(DocumentDto documentDto);
        Task DeleteDocumentAsync(int id);
    }
}
