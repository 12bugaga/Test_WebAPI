using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Infrastructure.Entity;

namespace Service.Interfaces
{
    public interface ITypeDocumentService
    {
        bool TryGetTypeDocument(int idTypeDoc, out TypeDocument TypeDocument);
        bool CheckOldDocument(TypeDocument entity, out int idDocFile);
        void AddTypeDoc(TypeDocument entity);
        void ChangeTypeDoc(TypeDocument entity);
        int Delete(int idTypeDoc);
    }
}
