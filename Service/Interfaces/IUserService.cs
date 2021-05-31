using System;
using Infrastructure.Entity;
using System.Collections.Generic;
using System.Text;

namespace Service.Interfaces
{
    public interface IUserService
    {
        bool TryGetAllUser(out IEnumerable<User> allUser);
        bool TryGetUserOnId(int idUser, out User user);
        bool MethodForAdmin(int idUser);
        bool ChangeRoleUser(int idUser);
        void AddUser(User entity);
        void ChangeUser(User entity);
        void DeleteUser(int idUser);
    }
}

