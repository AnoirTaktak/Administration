using Administration.Dtos;
using Administration.Models;
using Administration.Services;
using Administration.Services.Document;
using Administration.Services.Societe;
using AutoMapper;
using iText.Kernel.Pdf;
using Microsoft.AspNetCore.Mvc;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Drawing;
using PdfSharp.Drawing;
using PdfSharp.Pdf.IO;
using PdfSharp.Drawing;
using PdfSharp.Pdf.IO;
using iText.Kernel.Pdf;
using Administration.Services.Employe;


namespace Administration.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocument_Service _documentService;
        private readonly ISociete_Service _societeService;
        private readonly IEmploye_Service _employeService;
        private readonly IMapper _mapper;

        private new List<string> _allowedExtensions = new List<string> { ".pdf" };

        public DocumentController(IDocument_Service documentService, IMapper mapper,ISociete_Service societeService,IEmploye_Service employe_Service)
        {
            _documentService = documentService;
            _societeService = societeService;
            _employeService = employe_Service;
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<IActionResult> CreateDocument([FromForm] DocumentDto documentDto)
        {
            // Validation du fichier PDF
            if (documentDto.Doc_Pdf == null || documentDto.Doc_Pdf.Length == 0)
            {
                return BadRequest("Aucun fichier reçu ou fichier vide.");
            }
            if (!string.IsNullOrEmpty(documentDto.Doc_Pdf?.FileName) &&
                !_allowedExtensions.Contains(Path.GetExtension(documentDto.Doc_Pdf.FileName).ToLower()))
            {
                return BadRequest("Seul le format PDF est autorisé.");
            }

            // Récupération de la société
            var societe = await _societeService.GetSocieteById(documentDto.ID_Societe);
            if (societe == null || societe.CachetSignature == null || societe.CachetSignature.Length == 0)
            {
                return BadRequest("Société invalide ou CachetSignature introuvable.");
            }

            // Récupération de l'employé
            var employe = await _employeService.GetEmployeById(documentDto.ID_Employe);
            if (employe == null)
            {
                return BadRequest("Employé introuvable.");
            }

            // Générer le contenu du document
            var contenu = await _documentService.GenerateDocumentContent(documentDto);
            Console.WriteLine("Contenu généré : " + contenu);



            // Charger le fichier PDF
            byte[] originalPdf;
            using (var datastream = new MemoryStream())
            {
                await documentDto.Doc_Pdf.CopyToAsync(datastream);
                originalPdf = datastream.ToArray();
            }

            // Modification du PDF
            byte[] modifiedPdf;
            using (var inputPdfStream = new MemoryStream(originalPdf))
            using (var outputPdfStream = new MemoryStream())
            {
                // Charger le document PDF existant
                var pdfDoc = PdfSharp.Pdf.IO.PdfReader.Open(inputPdfStream, PdfDocumentOpenMode.Modify);

                // Accéder à la première page
                var page = pdfDoc.Pages[0];

                // Ajouter du contenu texte
                var yPosition = 100; // Initialiser la position verticale
                try
                {
                    using (var graphics = XGraphics.FromPdfPage(page))
                    {
                        // Définir la police et la couleur
                        var font = new XFont("Arial", 12);
                        var brush = XBrushes.Black;

                        // Définir une largeur maximale pour le texte (par exemple, la largeur de la page moins les marges)
                        double maxWidth = page.Width - 100; // 50 de marge de chaque côté

                        // Diviser le contenu en lignes pour éviter les coupures
                        var lignes = contenu.Replace("\r\n", "\n").Split('\n');

                        foreach (var ligne in lignes)
                        {
                            // Utiliser MeasureString pour gérer les retours automatiques à la ligne
                            var layoutText = WrapText(graphics, ligne.Trim(), font, maxWidth);
                            foreach (var wrappedLine in layoutText)
                            {
                                graphics.DrawString(wrappedLine, font, brush, new XRect(50, yPosition, maxWidth, 20), XStringFormats.TopLeft);
                                yPosition += 20; // Espacement entre chaque ligne
                            }
                        }

                        // Ajouter une ligne vide pour séparer l'image
                        yPosition += 20;
                    }

                }
                catch (Exception ex)
                {
                    return BadRequest("Erreur lors de l'ajout du contenu texte : " + ex.Message);
                }

                // Ajouter l'image de cachet et signature
                try
                {
                    using (var cachetSignatureStream = ConvertToCompatibleImageStream(societe.CachetSignature))
                    {
                        var cachetSignatureImage = XImage.FromStream(cachetSignatureStream);

                        using (var graphics = XGraphics.FromPdfPage(page))
                        {
                            // Ajouter l'image sous le texte (à la position yPosition)
                            graphics.DrawImage(cachetSignatureImage, 50, yPosition, 150, 100); // Ajuster taille et position
                        }
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest("Erreur lors de l'ajout de l'image CachetSignature : " + ex.Message);
                }

                // Sauvegarder le PDF modifié
                pdfDoc.Save(outputPdfStream);
                modifiedPdf = outputPdfStream.ToArray();
            }






            // Créer le document avec le contenu généré
            var document = _mapper.Map<Document>(documentDto);
            document.Contenu = contenu;  // Ajout du contenu généré au document
            document.Doc_Pdf = modifiedPdf;

            var createdDocument = await _documentService.CreateDocumentAsync(document);
            var data = _mapper.Map<Document>(createdDocument);

            // Retourner directement le fichier PDF généré après sa création
            return Ok(new { FileContents = Convert.ToBase64String(modifiedPdf), FileName = $"document_{data.ID_Document}.pdf" });
        }


        private List<string> WrapText(XGraphics graphics, string text, XFont font, double maxWidth)
        {
            var wrappedLines = new List<string>();
            var words = text.Split(' ');

            string currentLine = string.Empty;
            foreach (var word in words)
            {
                var testLine = string.IsNullOrEmpty(currentLine) ? word : currentLine + " " + word;

                // Mesurer la largeur de la ligne en cours
                var size = graphics.MeasureString(testLine, font);

                if (size.Width <= maxWidth)
                {
                    currentLine = testLine;
                }
                else
                {
                    wrappedLines.Add(currentLine);
                    currentLine = word; // Commencer une nouvelle ligne
                }
            }

            // Ajouter la dernière ligne si elle n'est pas vide
            if (!string.IsNullOrEmpty(currentLine))
            {
                wrappedLines.Add(currentLine);
            }

            return wrappedLines;
        }


        private MemoryStream ConvertToCompatibleImageStream(byte[] imageData)
        {
            using (var originalStream = new MemoryStream(imageData))
            {
                var image = System.Drawing.Image.FromStream(originalStream); // Utilise System.Drawing
                var compatibleStream = new MemoryStream();
                image.Save(compatibleStream, System.Drawing.Imaging.ImageFormat.Png); // Conversion en PNG
                compatibleStream.Seek(0, SeekOrigin.Begin); // Repositionner au début du flux
                return compatibleStream;
            }
        }


        private XImage LoadImage(byte[] imageData)
        {
            using (var stream = new MemoryStream(imageData))
            {
                return XImage.FromStream(stream);
            }
        }



        [HttpPut]
        public async Task<IActionResult> UpdateDocument(Document documentDto)
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
