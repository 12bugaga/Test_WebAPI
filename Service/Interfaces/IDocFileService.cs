using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using Infrastructure.Entity;
using Microsoft.AspNetCore.Http;

namespace Service.Interfaces
{
    public interface IDocFileService
    {
        bool GetFile(int idTypeDoc, out string file);

        void AddDocFile(IFormFile docFile, out int idCreateFile);
        void ChangeDocFile(int idDocFile, IFormFile docFile);
        void Delete(int idDocFile);
    }
}
