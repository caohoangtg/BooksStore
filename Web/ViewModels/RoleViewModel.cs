using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class RoleViewModel
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public virtual List<UserViewModel> Users { get; set; }
    }
}