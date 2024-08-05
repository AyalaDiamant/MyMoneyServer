using Logic.DTO;
using Logic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ServiceModel.Channels;

namespace MoneySystemServer.Controllers
{
    public class DocumentController : GlobalController
    {
        private IDocumentService documentService;
        public DocumentController(IDocumentService documentService)
        {
            this.documentService = documentService;
        }

        [HttpPost]
        public GResult<List<DocumentDTO>> GetDocuments(SerchDocument searchDoc)
        {
            return Success(documentService.GetDocuments(UserId.Value, searchDoc));
        }

        [HttpPost]
        public Result AddDocument([FromForm] FileFromClient file)
        {
            var request = Request;

            if (request.Form != null && request.Form.Files != null && request.Form.Files.Count > 0 && request.Form.Files[0] != null && request.Form.Files[0].Length > 0)
            {
                DocumentDTO document;
                byte[] data = null;

                using (var ms = new MemoryStream())
                {
                    request.Form.Files[0].CopyTo(ms);
                    data = ms.ToArray();
                }

                document = new DocumentDTO()
                {
                    Content = data,
                    Description = file.Description,
                    FileName = request.Form.Files[0].FileName,
                    UserId = UserId.Value
                };

                var isDocumentExist = documentService.AddDocument(document);
                if (!isDocumentExist)
                {
                    return Fail(value: -1);
                }
                return Success();
            }
            return Fail(value: 0);

        }

        [HttpDelete("{id}")]
        public Result DeleteDocument(int id)
        {
            var isDocumentExist = documentService.DeleteDocument(id, UserId.Value);
            if (!isDocumentExist)
            {
                return Fail(message: "document not found");
            }
            return Success();
        }

        [HttpPut]
        public Result UpdateDocument(IdName doc)
        {
            var success = documentService.UpdateDocument(doc);

            if (!success)

                return Fail();

            return Success();
        }
   
   

    }
}
