using Data.Infrastructure;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class RoleRepository : RepositoryBase<Roles>, IRoleRepository
    {
        public RoleRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }

    public interface IRoleRepository : IRepository<Roles>
    {
    }
}
