using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public virtual List<ReplyViewModel> Replys { get; set; }
        public virtual List<CommentViewModel> Comments { get; set; }
        public virtual List<MarkViewModel> Marks { get; set; }
        public virtual List<PostViewModel> Posts { get; set; }
        public virtual RoleViewModel Role { get; set; }
        public virtual ProfileViewModel Profile { get; set; }
    }
}