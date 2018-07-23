using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class ReplyViewModel
    {
        public int ReplyId { get; set; }
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public string RepComment { get; set; }
        public DateTime TimeRep { get; set; }
        public virtual CommentViewModel Comment { get; set; }
        public virtual UserViewModel User { get; set; }

        public ReplyViewModel()
        {
            TimeRep = DateTime.Now;
        }
    }
}