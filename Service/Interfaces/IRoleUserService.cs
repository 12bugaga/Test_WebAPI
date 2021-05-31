using Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interfaces
{
    public interface IRoleUserService
    {
        RoleUser GetRoleUser(int idUser);
        void AddRole(RoleUser entity);
        void ChangeRole(RoleUser entity);
        void Delete(int idUser);
    }
}
