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
    public interface IRoleService
    {
        IEnumerable<Roles> GetRoles();
        Roles GetRole(int? id);
        void CreateRole(Roles Role);
        void EditRole(Roles Role);
        void SaveRole();
        void DeleteRole(int id);
    }

    public class RoleService : IRoleService
    {
        private readonly IRoleRepository RoleRepository;
        private readonly IUnitOfWork unitOfWork;

        public RoleService(IRoleRepository RoleRepository, IUnitOfWork unitOfWork)
        {
            this.RoleRepository = RoleRepository;
            this.unitOfWork = unitOfWork;
        }

        public void CreateRole(Roles Role)
        {
            RoleRepository.Add(Role);
        }

        public void EditRole(Roles Role)
        {
            RoleRepository.Edit(Role);
        }

        public Roles GetRole(int? id)
        {
            var Role = RoleRepository.GetById(id);
            return Role;
        }

        public IEnumerable<Roles> GetRoles()
        {
            var Roles = RoleRepository.GetAll();
            return Roles;
        }

        public void SaveRole()
        {
            unitOfWork.Commit();
        }
        public void DeleteRole(int id)
        {
            var Role = RoleRepository.GetById(id);
            RoleRepository.Delete(Role);
        }
    }
}
