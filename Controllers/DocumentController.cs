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
            // **1. Validation du fichier PDF**
            if (documentDto.Doc_Pdf == null || documentDto.Doc_Pdf.Length == 0)
                return BadRequest("Aucun fichier reçu ou fichier vide.");

            if (!string.IsNullOrEmpty(documentDto.Doc_Pdf?.FileName) &&
                !_allowedExtensions.Contains(Path.GetExtension(documentDto.Doc_Pdf.FileName).ToLower()))
                return BadRequest("Seul le format PDF est autorisé.");

            // **2. Récupération des informations de la société**
            var societe = await _societeService.GetSocieteById(documentDto.ID_Societe);
            if (societe == null || societe.CachetSignature == null || societe.CachetSignature.Length == 0)
                return BadRequest("Société invalide ou CachetSignature introuvable.");

            // **3. Récupération des informations de l'employé**
            var employe = await _employeService.GetEmployeById(documentDto.ID_Employe);
            if (employe == null)
                return BadRequest("Employé introuvable.");

            // **4. Générer le contenu du document**
            var contenu = await _documentService.GenerateDocumentContent(documentDto);
            Console.WriteLine("Contenu généré : " + contenu);

            // **5. Charger le fichier PDF reçu**
            byte[] originalPdf;
            using (var datastream = new MemoryStream())
            {
                await documentDto.Doc_Pdf.CopyToAsync(datastream);
                originalPdf = datastream.ToArray();
            }

            // **6. Modification du PDF (ajout de texte et image CachetSignature)**
            byte[] modifiedPdf;
            using (var inputPdfStream = new MemoryStream(originalPdf))
            using (var outputPdfStream = new MemoryStream())
            {
                // Charger le document PDF existant
                var pdfDoc = PdfSharp.Pdf.IO.PdfReader.Open(inputPdfStream, PdfDocumentOpenMode.Modify);
                var page = pdfDoc.Pages[0]; // Accéder à la première page

                try
                {
                    using (var graphics = XGraphics.FromPdfPage(page))
                    {
                        // Configuration des polices et styles
                        var font = new XFont("Arial", 12);
                        var brush = XBrushes.Black;
                        double maxWidth = page.Width - 100; // Largeur maximale (avec marges)
                        var yPosition = 100; // Position verticale initiale

                        // Ajout du contenu texte
                        var lignes = contenu.Replace("\r\n", "\n").Split('\n');
                        int lineCount = 0;

                        foreach (var ligne in lignes)
                        {
                            if (lineCount < 6)
                            {
                                // Espacement entre les lignes
                                var layoutText = WrapText(graphics, ligne.Trim(), font, maxWidth);
                                foreach (var wrappedLine in layoutText)
                                {
                                    graphics.DrawString(wrappedLine, font, brush, new XRect(50, yPosition, maxWidth, 20), XStringFormats.TopLeft);
                                    yPosition += 20; // Espacement entre les lignes
                                }
                            }
                            else if (lineCount == 8)
                            {
                                // Espacement de deux lignes
                                yPosition += 40;

                                // Définir une police plus grande avec style gras
                                var specialFont = new XFont("Arial", 16); // Pas de "Bold" ici
                                var specialBrush = XBrushes.Black;

                                // Centrer la ligne
                                var layoutText = WrapText(graphics, ligne.Trim(), specialFont, maxWidth);
                                foreach (var wrappedLine in layoutText)
                                {
                                    // Dessiner le texte
                                    var rect = new XRect(50, yPosition, maxWidth, 20);
                                    graphics.DrawString(wrappedLine, specialFont, specialBrush, rect, XStringFormats.Center);

                                    // Dessiner une ligne pour souligner
                                    var textWidth = graphics.MeasureString(wrappedLine, specialFont).Width;
                                    var underlineY = yPosition + specialFont.Height + 2; // Position sous le texte
                                    var underlineX = (maxWidth - textWidth) / 2 + 50;    // Centrer la ligne

                                    graphics.DrawLine(XPens.Black, underlineX, underlineY, underlineX + textWidth, underlineY);

                                    yPosition += 40; // Espacement entre les lignes
                                }
                            }


                            else if (lineCount > 5)
                            {
                                // Espacement de deux lignes après la sixième
                                if (lineCount == 6)
                                {
                                    yPosition += 40; // Espacement de deux lignes
                                }

                                // Alignement normal après la sixième ligne
                                var layoutText = WrapText(graphics, ligne.Trim(), font, maxWidth);
                                foreach (var wrappedLine in layoutText)
                                {
                                    graphics.DrawString(wrappedLine, font, brush, new XRect(50, yPosition, maxWidth, 20), XStringFormats.TopLeft);
                                    yPosition += 20; // Espacement entre les lignes
                                }
                            }

                            lineCount++;
                        }

                        // Ajout de l'image CachetSignature
                        yPosition += 20; // Ligne vide avant l'image
                        using (var cachetSignatureStream = ConvertToCompatibleImageStream(societe.CachetSignature))
                        {
                            var cachetSignatureImage = XImage.FromStream(cachetSignatureStream);
                            graphics.DrawImage(cachetSignatureImage, 50, yPosition, 150, 100); // Position et taille
                        }
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest("Erreur lors de la modification du PDF : " + ex.Message);
                }

                // Sauvegarder le PDF modifié
                pdfDoc.Save(outputPdfStream);
                modifiedPdf = outputPdfStream.ToArray();
            }

            // **7. Création du document dans la base de données**
            var document = _mapper.Map<Document>(documentDto);
            document.Contenu = contenu; // Ajout du contenu généré
            document.Doc_Pdf = modifiedPdf;

            var createdDocument = await _documentService.CreateDocumentAsync(document);
            var data = _mapper.Map<Document>(createdDocument);

            // **8. Retourner le fichier PDF généré au frontend**
            return Ok(new
            {
                FileContents = Convert.ToBase64String(modifiedPdf),
                FileName = $"document_{data.ID_Document}.pdf"
            });
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
