using Data.Infrastructure;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class UserRepository : RepositoryBase<Users>, IUserRepository
    {
        public UserRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }

    public interface IUserRepository : IRepository<Users>
    {
    }
}
