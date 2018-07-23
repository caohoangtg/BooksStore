using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Comments
    {
        [Key]
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
        public DateTime TimeComment { get; set; }
        public virtual Posts Post { get; set; }
        public virtual Users User { get; set; }
        public virtual List<Replys> Replys { get; set; }
        public Comments()
        {
            TimeComment = DateTime.Now;
        }
    }
}
