using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ForumEntities: DbContext
    {
        public ForumEntities(): base("ForumEntity")
        {

        }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Marks> Marks { get; set; }
        public DbSet<Posts> Posts { get; set; }
        public DbSet<Profiles> Profiles { get; set; }
        public DbSet<Replys> Replys { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Topics> Topics { get; set; }
        public DbSet<Users> Users { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }
    }
}
