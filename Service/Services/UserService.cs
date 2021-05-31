using Infrastructure.Entity;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool TryGetUserOnId(int idUser, out User user)
        {
            user = _unitOfWork.Context.Set<User>().Find(idUser);
            if (user == null)
                return false;
            else
            {
                return true;
            }
        }

        public bool TryGetAllUser(out IEnumerable<User> allUser)
        {
            allUser= _unitOfWork.Context.Set<User>().AsEnumerable<User>();
            if (allUser.Count() == 0)
                return false;
            else
                return true;
        }
        /*
        public bool ChangeRoleUserOnAdmin(int idUser)
        {
            return ChangeRoleUser(idUser);
        }

        public bool ChangeRoleUserOnUser(int idUser)
        {
            return ChangeRoleUser(idUser);
        }
        */
        public bool MethodForAdmin(int idUser)
        {
            TryGetUserOnId(idUser, out User user);
            if (user.KodRole == 2)
                return true;
            else
                return false;
        }

        public void AddUser(User entity)
        {
            entity.Password = GetHashPassword(entity.Password);
            _unitOfWork.Context.Add(entity);
            _unitOfWork.Save();
        }

        public void ChangeUser(User entity)
        {
            TryGetUserOnId(entity.Id, out User user);
            user.UserName = entity.UserName;
            user.Password = GetHashPassword(entity.Password);
            user.Email= entity.Email;
            user.Status = entity.Status;
            user.KodRole = entity.KodRole;
            _unitOfWork.Save();
        }

        public void DeleteUser(int idUser)
        {
            TryGetUserOnId(idUser, out User user);
            _unitOfWork.Context.Remove(user);
            _unitOfWork.Save();
        }

        public bool ChangeRoleUser(int idUser)
        {
            if (TryGetUserOnId(idUser, out User user))
            {
                if (user.KodRole == 1)
                    user.KodRole = 2;
                else
                    user.KodRole = 1;
                _unitOfWork.Save();
                return true;
            }
            else
                return false;
        }

        private string GetHashPassword(string passw)
        {
            MD5 md5Hasher = MD5.Create();
            var hash = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(passw));
            return Convert.ToBase64String(hash);
        }
    }
}
