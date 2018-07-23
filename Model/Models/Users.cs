using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        //public int ProfileId { get; set; }
        public virtual List<Replys> Replys { get; set; }
        public virtual List<Comments> Comments { get; set; }
        public virtual List<Marks> Marks { get; set; }
        public virtual List<Posts> Posts { get; set; }
        //public virtual Profiles Profile { get; set; }
        public virtual Roles Role { get; set; }
        [ForeignKey("UserId")]
        public virtual Profiles Profile { get; set; }
    }
}
