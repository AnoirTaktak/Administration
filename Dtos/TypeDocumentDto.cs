namespace Administration.Dtos
{
    public class TypeDocumentDto
    {
        public required string NomType { get; set; } // Nom du type (ex: Attestation de Travail)
        public required string Template { get; set; } // Template pour le document
    }
}
