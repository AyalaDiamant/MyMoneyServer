using Logic.DTO;
using Logic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Words;
using Aspose.Words.Saving;
using Aspose.Cells;
using Aspose.Cells.Rendering;


namespace MoneySystemServer.Controllers
{

    [AllowAnonymous]
    public class FileController : GlobalController
    {
        private IDocumentService documentService;
        private IManagerDesignService managerDesignService;

        public FileController(IDocumentService documentService, IManagerDesignService managerDesignService)
        {
            this.documentService = documentService;
            this.managerDesignService = managerDesignService;
        }

        [HttpGet("{id}")]
        public ActionResult ShowFile(int id)
        {
            var x = Preview(id);
            if (x == null)
            {
                var file = documentService.GetFile(id);

                file.ContentType = GetContentType(file.FileName);

                return File(file.Content, file.ContentType);
            }
            return x;
        }


        [HttpGet("{idString}")]
        public ActionResult ShowFileDesign(string idString)
        {

            int id = RemoveTrailingZeros(idString);
            var file = managerDesignService.GetFile(id);
            var contentType = GetContentType(file.FileName);
            return File(file.ImageContent, contentType);
        }

        public static int RemoveTrailingZeros(string str)
        {
            string numberStr = str;
            string sign = "!";
            int index = numberStr.IndexOf(sign);

            if (index != -1)
            {
                return int.Parse(numberStr.Substring(0, index));
            }

            return int.Parse(str);
        }
        public ActionResult Preview(int id)
        {
            var file = documentService.GetFile(id);
            if (GetContentType(file.FileName) == "application/pdf")
            {
                var pdf = PreviewPDF(id, file);
                return pdf;
            }
            if (GetContentType(file.FileName) == "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
            {
                var doc = PreviewDOC(id, file);
                return doc;
            }
            if (GetContentType(file.FileName) == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                var xsl = PreviewXSL(id, file);
                return xsl;
            }
            return null;
        }

        // אם גט מצליח לקבל שתי נתונים להפוך את הפונקציה לגלובלית בערך ככה
        //[HttpGet("{id}")]
        //public ActionResult ShowFile(int id)
        //{
        //    // string fileName = string.Empty;
        //    // byte[] content = string.Empty;

        //    var file = documentService.GetFile(id);
        //    //var file = managerService.GetFile();//byte[], contentType

        //    var contentType = GetContentType(file.FileName);
        //    return File(file.Content, contentType);
        //}
        public ActionResult PreviewPDF(int id, DocumentDTO file)
        {
            using (MemoryStream ms = new MemoryStream(file.Content))
            {
                // Load PDF document
                Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(ms);

                using (MemoryStream imageStream = new MemoryStream())
                {
                    // Create an instance of the PNG device with the specified attributes
                    var device = new PngDevice(96, 96); // 96 dpi

                    // Convert the first page of the PDF to an image
                    device.Process(pdfDocument.Pages[1], imageStream);

                    // Return the image file
                    imageStream.Position = 0; // Reset stream position before returning
                    return File(imageStream.ToArray(), "image/png");
                }
            }
        }
        public ActionResult PreviewXSL(int id, DocumentDTO file)
        {

            using (MemoryStream ms = new MemoryStream(file.Content))
            {
                Workbook workbook = new Workbook(ms);
                Worksheet sheet = workbook.Worksheets[0]; // Process the first worksheet

                using (MemoryStream imageStream = new MemoryStream())
                {
                    // Set image options
                    var options = new ImageOrPrintOptions
                    {
                        ImageType = Aspose.Cells.Drawing.ImageType.Png, // Specify the image type
                        HorizontalResolution = 96,
                        VerticalResolution = 96
                    };

                    // Create a renderer for the worksheet
                    var sheetRender = new SheetRender(sheet, options);

                    // Render the first page of the worksheet to the MemoryStream
                    sheetRender.ToImage(0, imageStream);

                    // Return the image file
                    imageStream.Position = 0; // Reset stream position before returning
                    return File(imageStream.ToArray(), "image/png");
                }
            }

        }
        public ActionResult PreviewDOC(int id, DocumentDTO file)
        {
            using (MemoryStream ms = new MemoryStream(file.Content))
            {
                Aspose.Words.Document doc = new Aspose.Words.Document(ms);

                using (MemoryStream imageStream = new MemoryStream())
                {
                    // Set image options using the explicit namespace
                    var options = new Aspose.Words.Saving.ImageSaveOptions(Aspose.Words.SaveFormat.Png)

                    {
                        PageSet = new PageSet(0), // Render the first page
                        Resolution = 96
                    };

                    // Save the document page as an image
                    doc.Save(imageStream, options);

                    // Return the image file
                    imageStream.Position = 0; // Reset stream position before returning
                    return File(imageStream.ToArray(), "image/png");
                }
            }
        }

        private string GetContentType(string fileName)
        {
            string type = "application/pdf";
            var extention = Path.GetExtension(fileName);
            if (extention == ".png" || extention == ".PNG")
            {
                type = "image/png";
            }
            else if (extention == ".jpg" || extention == ".JPG")
            {
                type = "image/jpg";
            }
            else if (extention == ".docx" || extention == ".doc")
            {
                type = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            }
            else if (extention == ".xlsx" || extention == ".xls")
            {
                type = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            }

            return type;
        }
    }
}
