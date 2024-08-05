using iTextSharp.text;
using iTextSharp.text.pdf;
using Logic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iTextSharp.text.pdf.AcroFields;

namespace Logic.Services
{
    public interface IPDFService
    {
        MemoryStream DownloadFileBase();
        MemoryStream DownloadMovingsPDF(Search search, int userId);
    }
    public class PDFService : IPDFService
    {
        private static BaseFont basefont = BaseFont.CreateFont(Path.GetFullPath("Fonts/GISHA.ttf"), BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

        private Image GIF = Image.GetInstance("Imgs/logo.JPG");
        private IMovingService movingService;
        public PDFService(IMovingService movingService)
        {
            this.movingService = movingService;
        }

        public MemoryStream DownloadMovingsPDF(Search search, int userId)
        {
            PdfPTable title = CreateTitle("תנועות");
            title.WidthPercentage = 100;
            PdfPTable table = CreateTableMovings(search, userId);
            table.WidthPercentage = 100;
            var stream = CreatePdfDocument(title, table);

            return stream;
        }
        private PdfPTable CreateTableMovings(Search search, int userId)
        {
            var data = movingService.GetMovings(search, userId);
            PdfPTable table = new PdfPTable(6);
            table.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            table.SpacingBefore = 10;
            table.SpacingAfter = 10;

            // הגדרת עמודות רוחב
            float[] columnWidths = new float[] { 25f, 25f, 25f, 25f, 25f, 25f };
            table.SetWidths(columnWidths);

            // כותרות של הטבלה בכתב בולט
            AddCell(table, "תאריך", 12, Font.BOLD);
            AddCell(table, "סה''כ", 12, Font.BOLD);
            AddCell(table, "תחום", 12, Font.BOLD);
            AddCell(table, "אופן תשלום", 12, Font.BOLD);
            AddCell(table, "הערות", 12, Font.BOLD);
            AddCell(table, " ", 12, Font.BOLD);

            foreach (var item in data)
            {
                AddCell(table, item.Date.ToString());
                AddCell(table, item.Sum.ToString());
                AddCell(table, item.UserArea.Name);
                AddCell(table, item.PayOption.Name);
                AddCell(table, item.Common.ToString());
                AddCell(table, " ");
            }

            return table;
        }


        public MemoryStream DownloadFileBase()
        {
            PdfPTable title = CreateTitle("כותרת ראשית");
            title.WidthPercentage = 100;
            PdfPTable table = CreateTableBase();
            table.WidthPercentage = 100;
            var stream = CreatePdfDocument(title, table);

            return stream;
        }
        private PdfPTable CreateTitle(string header, bool isSpace = false)
        {
            Font gisha = new Font(basefont, 30, Font.BOLD, BaseColor.BLACK);

            PdfPTable title = new PdfPTable(1);
            title.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            PdfPCell cell = new PdfPCell(new Phrase(header, gisha))
            {
                Border = 0,
                HorizontalAlignment = Element.ALIGN_CENTER,
                PaddingBottom = 10
            };
            title.AddCell(cell);

            if (isSpace)
            {
                PdfPCell space = new PdfPCell(new Phrase(" ", gisha))
                {
                    Border = 0,
                    PaddingBottom = 10
                };
                title.AddCell(space);
            }

            return title;
        }


        private PdfPTable CreateTableBase()
        {
            PdfPTable table = new PdfPTable(1);
            table.RunDirection = PdfWriter.RUN_DIRECTION_RTL;


            AddCell(table, " ");
            AddCell(table, "שלח את תהטקסט לכאן");
            AddCell(table, " הטקסט שלך כאן");
            AddCell(table, " ");
            AddCell(table, " בברכה");
            return table;

        }

        private MemoryStream CreatePdfDocument(PdfPTable title, PdfPTable table = null, List<PdfPTable> list = null)
        {
            var document = new iTextSharp.text.Document(new Rectangle(288f, 144f), 20, 50, 20, 50);
            document.SetPageSize(PageSize.A4);

            var stream = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, stream);

            title.WidthPercentage = 100;
            if (table != null)
            {
                table.WidthPercentage = 100;
            }

            document.Open();

            document.Add(GIF);

            document.Add(title);
            document.Add(new Paragraph("   "));
            if (table != null)
            {
                document.Add(table);
            }
            if (list != null)
            {
                foreach (var item in list)
                {
                    item.WidthPercentage = 100;
                    document.Add(new Paragraph("   "));
                    document.Add(item);
                    document.Add(new Paragraph("   "));
                }
            }

            document.Add(new Paragraph("   "));
            document.Close();


            return stream;
        }

        private void AddCell(PdfPTable table, string value, int size = 12, int isBold = 0)
        {
            Font gisha = new Font(basefont, size, isBold, BaseColor.BLACK);
            PdfPCell cell = new PdfPCell(new Phrase(value, gisha));
            cell.Border = 0;
            table.AddCell(cell);
        }


    }
}
