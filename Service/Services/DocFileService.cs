using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Infrastructure.Entity;
using Microsoft.AspNetCore.Http;
using Service.Interfaces;

namespace Service.Services
{
    public class DocFileService : IDocFileService
    {
        private readonly IUnitOfWork _unitOfWork;
        public DocFileService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddDocFile(IFormFile docFile, out int idCreateFile)
        {
            byte[] fileByte = GetByteFile(docFile);
            DocumentFile file = new DocumentFile {File = fileByte };
            _unitOfWork.Context.Add(file);
            _unitOfWork.Save();
            IEnumerable<DocumentFile> allDocFile = _unitOfWork.Context.Set<DocumentFile>().AsEnumerable<DocumentFile>();
            idCreateFile = allDocFile.Last().Id;
        }

        public void ChangeDocFile(int idDocFile, IFormFile docFile)
        {
            byte[] fileByte = GetByteFile(docFile);
            GetDocumentFile(idDocFile).File = fileByte;
            _unitOfWork.Save();
        }

        public void Delete(int idTypeDoc)
        {
            _unitOfWork.Context.Remove(GetDocumentFile(idTypeDoc));
            _unitOfWork.Save();
        }

        public bool GetFile(int idTypeDoc, out string fileString)
        {
            DocumentFile docFile = _unitOfWork.Context.Set<DocumentFile>().Find(idTypeDoc);
            fileString = null;
            if(docFile != null)
            { 
                //fileString = BitConverter.ToString(docFile.File);
                fileString = Encoding.UTF8.GetString(docFile.File);
                return true;
            }
            else 
                return false;
        }


        private DocumentFile GetDocumentFile(int idTypeDoc)
        {
            return _unitOfWork.Context.Set<DocumentFile>().Find(idTypeDoc);
        }

        private byte[] GetByteFile(IFormFile docFile)
        {
            string strFile = null;
            if (docFile.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    docFile.CopyTo(ms);
                    var docFileBytes= ms.ToArray();
                    strFile = Convert.ToBase64String(docFileBytes);
                }
            }
            byte[] bytesFile = Encoding.ASCII.GetBytes(strFile);
            return bytesFile;
        }
    }
}
