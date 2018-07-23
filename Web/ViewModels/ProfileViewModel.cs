using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class ProfileViewModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string ImageData { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        public ProfileViewModel()
        {
            FullName = " ";
            Phone = " ";
            ImageData = "default.png";
            Birthday = DateTime.Now;
            Address = " ";
        }
    }
}