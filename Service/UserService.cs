using Data.Infrastructure;
using Data.Repositories;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IUserService
    {
        IEnumerable<Users> GetUsers();
        Users GetUser(int? id);
        void CreateUser(Users User);
        void EditUser(Users User);
        void SaveUser();
        void DeleteUser(int id);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository UserRepository;
        private readonly IUnitOfWork unitOfWork;

        public UserService(IUserRepository UserRepository, IUnitOfWork unitOfWork)
        {
            this.UserRepository = UserRepository;
            this.unitOfWork = unitOfWork;
        }

        public void CreateUser(Users User)
        {
            UserRepository.Add(User);
        }

        public void EditUser(Users User)
        {
            UserRepository.Edit(User);
        }

        public Users GetUser(int? id)
        {
            var User = UserRepository.GetById(id);
            return User;
        }

        public IEnumerable<Users> GetUsers()
        {
            var Users = UserRepository.GetAll();
            return Users;
        }

        public void SaveUser()
        {
            unitOfWork.Commit();
        }
        public void DeleteUser(int id)
        {
            var User = UserRepository.GetById(id);
            UserRepository.Delete(User);
        }
    }
}
