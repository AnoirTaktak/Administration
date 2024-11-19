using Administration.Dtos;
using Administration.Services;
using Administration.Services.Document;
using Microsoft.AspNetCore.Mvc;

namespace Administration.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocument_Service _documentService;

        public DocumentController(IDocument_Service documentService)
        {
            _documentService = documentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDocument([FromBody] DocumentDto documentDto)
        {
            var createdDocument = await _documentService.CreateDocumentAsync(documentDto);
            return Ok(createdDocument);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocumentById(int id)
        {
            var document = await _documentService.GetDocumentByIdAsync(id);
            if (document == null) return NotFound();

            return Ok(document);
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<IActionResult> GetDocumentsByEmployeeId(int employeeId)
        {
            var documents = await _documentService.GetDocumentsByEmployeeIdAsync(employeeId);
            return Ok(documents);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDocument([FromBody] DocumentDto documentDto)
        {
            var updatedDocument = await _documentService.UpdateDocumentAsync(documentDto);
            if (updatedDocument == null) return NotFound();

            return Ok(updatedDocument);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            await _documentService.DeleteDocumentAsync(id);
            return NoContent();
        }
    }
}
