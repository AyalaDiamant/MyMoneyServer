using Logic.DTO;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client.Extensions.Msal;
using OfficeOpenXml.Drawing;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Logic.Services
{
    public interface IDocumentService
    {
        List<DocumentDTO> GetDocuments(int CurrentUserId, SerchDocument searchDoc);
        bool AddDocument(DocumentDTO document);
        bool DeleteDocument(int id, int CurrentUserId);

        DocumentDTO GetFile(int id);

        bool UpdateDocument(IdName doc);
    }
    public class DocumentService : IDocumentService
    {
        IDBService dBService;
        private object dbService;

        public DocumentService(IDBService dBService)
        {
            this.dBService = dBService;
        }
        public List<DocumentDTO> GetDocuments(int currentUserId, SerchDocument searchDoc)
        
        {
            List<DocumentDTO> list = new List<DocumentDTO>();
            var q = dBService.entities.Documents.Where(x => x.UserId == currentUserId).ToList();
           if (searchDoc.Description != "" || searchDoc.Name != "")
            {
               if (searchDoc != null)
                {
             if (searchDoc.Description != null && searchDoc.Description != "")
                  {
                      q = q.Where(x => x.Description.Contains(searchDoc.Description)).ToList();
                  }
                if (searchDoc.Name != null && searchDoc.Name != "")
                 {
                     if(searchDoc.Description != "")
                     q.AddRange(q.Where(x => x.FileName.Contains(searchDoc.Name)).ToList());
                     else
                      q =   q.Where(x => x.FileName.Contains(searchDoc.Name)  ).ToList();

                  }
              }
            }

            list = q.Where(x => x.UserId == currentUserId && (x.Description.Contains(searchDoc.Description) || x.FileName.Contains(searchDoc.Name))).Select(x => new DocumentDTO()
            {
                Description = x.Description,
                FileName = x.FileName,
                Id = x.Id,
                Src = "File/ShowFile/" + x.Id
            }).ToList();
            return list;
        
        }
        //public File getImageXLXS(int userId)

        //{
        //    var x = dBService.entities.Documents.Where(x=>x.UserId == userId).FirstOrDefault().Content;
        //    using (var package = new ExcelPackage(new MemoryStream(x)))
        //    {
        //        var worksheet = package.Workbook.Worksheets[0];
        //        var image = worksheet.Drawings[0] as ExcelPicture;

        //        using (var thumbStream = new MemoryStream())
        //        {
        //           // image.Image.Save(thumbStream, ImageFormat.Png);
        //            return File(thumbStream.ToArray(), "image/png");
        //        }
             
        //    }
        //}
        public bool AddDocument(DocumentDTO document)
        {
            if (dBService.entities.Documents.Any(l => l.UserId == document.UserId && l.FileName == document.FileName))
            {
                return false;
            }
            dBService.entities.Documents.Add(new Document()
            {
                Content = document.Content,
                Description = document.Description,
                FileName = document.FileName,
                UserId = document.UserId

            });
            dBService.Save();
            return true;
        }
        public bool DeleteDocument(int id, int CurrentUserId)
        {
            var DocumentToDelete = dBService.entities.Documents.FirstOrDefault(x => x.Id == id);
            if (DocumentToDelete != null)
            {
                dBService.entities.Documents.Remove(dBService.entities.Documents.FirstOrDefault(x => x.Id == id));
                dBService.Save();
                return true;
            }
            return false;
        }

        public DocumentDTO GetFile(int id)
        {
            var doc = new DocumentDTO();
            var dbFile = dBService.entities.Documents.FirstOrDefault(x => x.Id == id);
            if (dbFile != null)
            {
                doc.Content = dbFile.Content;
                doc.FileName = dbFile.FileName;
            }
            return doc;
        }
        public bool UpdateDocument(IdName doc)
        {
            var dbDescrip = dBService.entities.Documents.FirstOrDefault(x => x.Id == doc.Id);
            if (dbDescrip != null)
            {
                dbDescrip.Description = doc.Name;
                dBService.Save();
                return true;
            }
            return false;
        }
        //public List<DocumentDTO> SearchDocument(SerchDocument searchDoc)
        //{
        //    var query = dBService.entities.Documents.Where(doc => doc.FileName.Contains(searchDoc.Description) || doc.FileName.Contains(searchDoc.Description)).ToList();
        //    if (query.Count > 0)
        //    {
        //        var result = new List<DocumentDTO>();
        //        foreach (var doc in query)
        //        {

        //        }
        //    }
        //    return query;

        //}

    }
}
