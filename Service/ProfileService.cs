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
    public interface IProfileService
    {
        IEnumerable<Profiles> GetProfiles();
        Profiles GetProfile(int? id);
        void CreateProfile(Profiles Profile);
        void EditProfile(Profiles Profile);
        void SaveProfile();
        void DeleteProfile(int id);
    }

    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository ProfileRepository;
        private readonly IUnitOfWork unitOfWork;

        public ProfileService(IProfileRepository ProfileRepository, IUnitOfWork unitOfWork)
        {
            this.ProfileRepository = ProfileRepository;
            this.unitOfWork = unitOfWork;
        }

        public void CreateProfile(Profiles Profile)
        {
            ProfileRepository.Add(Profile);
        }

        public void EditProfile(Profiles Profile)
        {
            ProfileRepository.Edit(Profile);
        }

        public Profiles GetProfile(int? id)
        {
            var Profile = ProfileRepository.GetById(id);
            return Profile;
        }

        public IEnumerable<Profiles> GetProfiles()
        {
            var Profiles = ProfileRepository.GetAll();
            return Profiles;
        }

        public void SaveProfile()
        {
            unitOfWork.Commit();
        }
        public void DeleteProfile(int id)
        {
            var Profile = ProfileRepository.GetById(id);
            ProfileRepository.Delete(Profile);
        }
    }
}
