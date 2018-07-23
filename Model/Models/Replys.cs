using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Replys
    {
        [Key]
        public int ReplyId { get; set; }
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public string RepComment { get; set; }
        public DateTime TimeRep { get; set; }
        public virtual Comments Comment { get; set; }
        public virtual Users User { get; set; }

        public Replys()
        {
            TimeRep = DateTime.Now;
        }
    }
}
