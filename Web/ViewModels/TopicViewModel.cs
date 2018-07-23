using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class TopicViewModel
    {
        public int TopicId { get; set; }
        public string Name { get; set; }
        public virtual List<PostViewModel> Posts { get; set; }
    }
}