using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class PostViewModel
    {
        public int PostId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime Time { get; set; }
        public int View { get; set; }
        public string File { get; set; }
        public bool Check { get; set; }
        public int CategoryId { get; set; }
        public int TopicId { get; set; }
        public int UserId { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }

        public virtual CategoryViewModel Category { get; set; }
        public virtual TopicViewModel Topic { get; set; }
        public virtual UserViewModel User { get; set; }
        public virtual List<CommentViewModel> Comments { get; set; }
        public virtual List<MarkViewModel> Marks { get; set; }

        public PostViewModel()
        {
            Time = DateTime.Now;
            View = 0;
            File = "";
            Check = true;
        }
    }
}