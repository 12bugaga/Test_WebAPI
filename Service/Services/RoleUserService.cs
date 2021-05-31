using Service.Interfaces;
using Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Services
{
    public class RoleUserService : IRoleUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public RoleUserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public RoleUser GetRoleUser(int idUser)
        {
            return _unitOfWork.Context.Set<RoleUser>().Find(idUser);
        }

        public void AddRole(RoleUser entity)
        {
            _unitOfWork.Context.Add(entity);
            _unitOfWork.Save();
        }

        public void ChangeRole(RoleUser entity)
        {
            GetRoleUser(entity.Id).Role = entity.Role;
            _unitOfWork.Save();
        }

        public void Delete(int idUser)
        {
            _unitOfWork.Context.Remove(GetRoleUser(idUser));
            _unitOfWork.Save();
        }

    }
}
