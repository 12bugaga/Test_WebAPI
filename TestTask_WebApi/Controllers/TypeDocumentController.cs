using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.Drawing;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace TestTask_WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TypeDocumentController : Controller
    {

        private readonly ITypeDocumentService repoTypeDocument;
        private readonly IDocFileService repoDocFile;

        public TypeDocumentController(ITypeDocumentService repoTypeDocument, IDocFileService repoDocFile)
        {
            this.repoTypeDocument = repoTypeDocument;
            this.repoDocFile = repoDocFile;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<User>))]
        public IActionResult GetTypeDoc(int id)
        {
            try
            {
                if (!repoTypeDocument.TryGetTypeDocument(id, out var typeDoc))
                    return NotFound();
                return Ok(typeDoc);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public HttpResponseMessage AddDocument([FromForm] int kodUser, [FromForm] string type, [FromForm] IFormFile docFile)
        {
            try
            {
                TypeDocument entity = new TypeDocument { KodUser = kodUser, Type = type};

                if (LocalValidator(entity))
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                }
                if(repoTypeDocument.CheckOldDocument(entity, out int idDocFile))
                {
                    repoDocFile.ChangeDocFile(idDocFile, docFile);
                }
                else
                {
                    repoDocFile.AddDocFile(docFile, out int idCreateFile);
                    entity.KodDocumentFile = idCreateFile;
                    repoTypeDocument.AddTypeDoc(entity);
                }
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public HttpResponseMessage DeleteTypeDoc(int id)
        {
            try
            {
                int idFile = repoTypeDocument.Delete(id);
                repoDocFile.Delete(idFile);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        private bool LocalValidator(TypeDocument entity)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(entity);
            if (!Validator.TryValidateObject(entity, context, results, true))
            {
                return true;
            }
            else
                return false;
        }
    }
}
