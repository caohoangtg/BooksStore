using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Models
{
    public class Posts
    {
        [Key]
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Time { get; set; }
        public int View { get; set; }
        public string File { get; set; }
        public bool Check { get; set; }
        public int CategoryId { get; set; }
        public int TopicId { get; set; }
        public int UserId { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        public virtual Categories Category { get; set; }
        public virtual Topics Topic { get; set; }
        public virtual Users User { get; set; }
        public virtual List<Comments> Comments { get; set; }
        public virtual List<Marks> Marks { get; set; }

        public Posts()
        {
            Time = DateTime.Now;
            View = 0;
            File = "";
            Check = true;
        }
    }
}
