using iTextSharp.text.pdf;
using Logic.DTO;
using Logic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneySystemServer.Controllers;

namespace Api.Controllers
{
   
    public class PDFController : GlobalController
    {
        private IPDFService pdfService;
        public PDFController(IPDFService pdfService)
        {
                this.pdfService = pdfService;
        }

        public ActionResult DownloadFileBase()
        {
            return Download(pdfService.DownloadFileBase(), "File.pdf");
        }

        public ActionResult DownloadMovingsPDF(Search search)
        {
            return Download(pdfService.DownloadMovingsPDF(search, UserId.Value), "Movings.pdf");
        }

        public ActionResult Download(MemoryStream stream, string fileName)
        {
            var ms = new MemoryStream(stream.ToArray());
            ms.Seek(0, SeekOrigin.Begin);

            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(ms.ToArray(), "application/pdf", fileName);
        }
    }
}
