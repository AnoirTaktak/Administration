using System.Collections.Generic;
using System.Threading.Tasks;
using Administration.Models;
using Administration.Services;
using Administration.Services.TypeDocument;
using Microsoft.AspNetCore.Mvc;

namespace Administration.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TypeDocumentController : ControllerBase
    {
        private readonly ITypeDoc_Service _typeDocumentService;

        public TypeDocumentController(ITypeDoc_Service typeDocumentService)
        {
            _typeDocumentService = typeDocumentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTypeDocument([FromBody] TypeDocument typeDocument)
        {
            var createdType = await _typeDocumentService.CreateTypeDocumentAsync(typeDocument);
            return Ok(createdType);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTypeDocumentById(int id)
        {
            var typeDocument = await _typeDocumentService.GetTypeDocumentByIdAsync(id);
            if (typeDocument == null) return NotFound();

            return Ok(typeDocument);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTypeDocuments()
        {
            var types = await _typeDocumentService.GetAllTypeDocumentsAsync();
            return Ok(types);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTypeDocument([FromBody] TypeDocument typeDocument)
        {
            var updatedType = await _typeDocumentService.UpdateTypeDocumentAsync(typeDocument);
            if (updatedType == null) return NotFound();

            return Ok(updatedType);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeDocument(int id)
        {
            await _typeDocumentService.DeleteTypeDocumentAsync(id);
            return NoContent();
        }
    }
}
