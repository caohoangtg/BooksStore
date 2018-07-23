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
    public class Profiles
    {

        //public int ProfileId { get; set; }
        [Key]
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string ImageData { get; set; }
        public DateTime Birthday { get; set; }
        public string Address{ get; set; }
        //public virtual Users User { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
        public Profiles()
        {
            FullName = " ";
            Phone = " ";
            ImageData = "default.png";
            Birthday = DateTime.Now;
            Address = " ";
        }
    }
    
}
