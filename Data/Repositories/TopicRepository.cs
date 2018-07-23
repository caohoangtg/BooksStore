using Data.Infrastructure;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class TopicRepository : RepositoryBase<Topics>, ITopicRepository
    {
        public TopicRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }

    public interface ITopicRepository : IRepository<Topics>
    {
    }
}
