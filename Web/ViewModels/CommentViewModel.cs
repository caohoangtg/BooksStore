using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class CommentViewModel
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
        public DateTime TimeComment { get; set; }
        public virtual PostViewModel Post { get; set; }
        public virtual UserViewModel User { get; set; }
        public virtual List<ReplyViewModel> Replys { get; set; }
        public CommentViewModel()
        {
            TimeComment = DateTime.Now;
        }
    }
}