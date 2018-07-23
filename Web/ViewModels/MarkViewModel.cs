using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class MarkViewModel
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public virtual PostViewModel Post { get; set; }
        public virtual UserViewModel User { get; set; }
    }
}