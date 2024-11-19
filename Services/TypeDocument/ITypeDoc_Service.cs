using TypeDocumentModel = Administration.Models.TypeDocument; // Création de l'alias


namespace Administration.Services.TypeDocument
{
    public interface ITypeDoc_Service
    {
        Task<TypeDocumentModel> CreateTypeDocumentAsync(TypeDocumentModel typeDocument);
        Task<TypeDocumentModel> GetTypeDocumentByIdAsync(int id);
        Task<IEnumerable<TypeDocumentModel>> GetAllTypeDocumentsAsync();
        Task<TypeDocumentModel> UpdateTypeDocumentAsync(TypeDocumentModel typeDocument);
        Task DeleteTypeDocumentAsync(int id);
    }
}
