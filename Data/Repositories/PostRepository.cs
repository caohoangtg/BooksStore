using Data.Infrastructure;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class PostRepository : RepositoryBase<Posts>, IPostRepository
    {
        public PostRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }

    public interface IPostRepository : IRepository<Posts>
    {
    }
}
