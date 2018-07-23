using Data.Infrastructure;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ReplyRepository : RepositoryBase<Replys>, IReplyRepository
    {
        public ReplyRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }

    public interface IReplyRepository : IRepository<Replys>
    {
    }
}
