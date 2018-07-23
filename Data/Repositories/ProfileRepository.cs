using Data.Infrastructure;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ProfileRepository : RepositoryBase<Profiles>, IProfileRepository
    {
        public ProfileRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }

    public interface IProfileRepository : IRepository<Profiles>
    {
    }
}
