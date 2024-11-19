using Administration.Models;
using Microsoft.EntityFrameworkCore;
using TypeDocumentModel = Administration.Models.TypeDocument; // Création de l'alias


namespace Administration.Services.TypeDocument
{
    public class TypeDoc_Service : ITypeDoc_Service
    {
        private readonly AppDBContext _Context;

        public TypeDoc_Service(AppDBContext dbContext)
        {
            _Context = dbContext;
        }

        public async Task<TypeDocumentModel> CreateTypeDocumentAsync(TypeDocumentModel typeDocument)
        {
            _Context.TypesDocuments.Add(typeDocument);
            await _Context.SaveChangesAsync();
            return typeDocument;
        }

        public async Task<TypeDocumentModel> GetTypeDocumentByIdAsync(int id)
        {
            return await _Context.TypesDocuments.FindAsync(id);
        }

        public async Task<IEnumerable<TypeDocumentModel>> GetAllTypeDocumentsAsync()
        {
            return await _Context.TypesDocuments.ToListAsync();
        }

        public async Task<TypeDocumentModel> UpdateTypeDocumentAsync(TypeDocumentModel typeDocument)
        {
            var existingType = await _Context.TypesDocuments.FindAsync(typeDocument.ID_TypeDocument);
            if (existingType == null) return null;

            existingType.NomType = typeDocument.NomType;
            existingType.Template = typeDocument.Template;

            await _Context.SaveChangesAsync();
            return existingType;
        }

        public async Task DeleteTypeDocumentAsync(int id)
        {
            var typeDocument = await _Context.TypesDocuments.FindAsync(id);
            if (typeDocument != null)
            {
                _Context.TypesDocuments.Remove(typeDocument);
                await _Context.SaveChangesAsync();
            }
        }
    }
}
