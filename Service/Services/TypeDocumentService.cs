using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Infrastructure.Entity;
using Service.Interfaces;

namespace Service.Services
{
    public class TypeDocumentService : ITypeDocumentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TypeDocumentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool TryGetTypeDocument(int idTypeDoc, out TypeDocument typeDoc)
        {
            typeDoc = _unitOfWork.Context.Set<TypeDocument>().Find(idTypeDoc);
            if (typeDoc == null)
                return false;
            else
            {
                return true;
            }
        }

        //Проверка на сущетвование данного типа файла и изображения
        public bool CheckOldDocument(TypeDocument entity, out int idDocFile)
        {
            idDocFile = 0;

            //IEnumerable<DocumentFile> allDocFile = _unitOfWork.Context.Set<DocumentFile>().AsEnumerable<DocumentFile>();

            var oldDocument = from typeDoc in _unitOfWork.Context.Set<TypeDocument>().AsEnumerable<TypeDocument>()
                              where typeDoc.KodUser == entity.KodUser && typeDoc.Type == entity.Type
                              select typeDoc;

            if (oldDocument.Count() == 0)
                return false;
            else
            {
                idDocFile = oldDocument.Last().KodDocumentFile;
                return true;
            }
        }


        public void AddTypeDoc(TypeDocument entity)
        {
            _unitOfWork.Context.Add(entity);
            _unitOfWork.Save();
        }

        public void ChangeTypeDoc(TypeDocument entity)
        {
            TryGetTypeDocument(entity.Id, out TypeDocument typeDoc);
            typeDoc.Type= entity.Type;
            _unitOfWork.Save();
        }

        public int Delete(int idTypeDoc)
        {
            TryGetTypeDocument(idTypeDoc, out TypeDocument typeDoc);
            int idDocFile = typeDoc.KodDocumentFile;
            _unitOfWork.Context.Remove(typeDoc);
            _unitOfWork.Save();
            return idDocFile;
        }
    }
}
