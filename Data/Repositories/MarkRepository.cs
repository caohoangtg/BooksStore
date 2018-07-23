using Data.Infrastructure;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class MarkRepository : RepositoryBase<Marks>, IMarkRepository
    {
        public MarkRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public Marks GetById(int Pid, int Uid)
        {
            return DbContext.Marks.Where(s => s.PostId == Pid && s.UserId == Uid).FirstOrDefault();
        }
    }

    public interface IMarkRepository : IRepository<Marks>
    {
        Marks GetById(int Pid, int Uid);
    }
}
