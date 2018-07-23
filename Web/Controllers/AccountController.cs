using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Data;
using Models;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        ForumEntities db = new ForumEntities();
        // GET: Account

        public ActionResult _LoginPartial()
        {
            var idU = 0;
            if (Session["UserId"] != null)
                idU = ((int)Session["UserId"]);
            else if (Session["AdminId"] != null)
                idU = ((int)Session["AdminId"]);
            if(idU !=0)
                ViewBag.Name = db.Users.Where(i => i.UserId == idU).Select(i => i.Name).FirstOrDefault();
            return PartialView();
        }

        //[HttpPost]
        //public ActionResult Login(Users user)
        //{
        //    var result = db.Users.Where(a => a.Email == user.Email && a.Password == user.Password).ToList();
        //    if(result.Count() > 0)
        //    {
        //        Session["UserId"] = result[0].UserId;
        //        FormsAuthentication.SetAuthCookie(result[0].Email, false);
        //        //if admin
        //        if(result[0].RoleId == 1)
        //        {
        //            return RedirectToAction("../Admin/Index");
        //        }
        //        //if user
        //        else if(result[0].RoleId == 2)
        //        {
        //            return RedirectToAction("../User/Index");
        //        }
        //    }
        //    else
        //    {
        //        ViewBag.Message = "Incorrect Email or Password";
        //    }
        //    return View(user);
        //}

        public ActionResult _PartialLogin()
        {
            return PartialView("_PartialLogin");
        }

        public ActionResult _ChangePasswordPartial()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult _PartialLogin(Users user)
        {
            var result = db.Users.Where(a => a.Email == user.Email && a.Password == user.Password).ToList();
            if (result.Count() > 0)
            {
                FormsAuthentication.SetAuthCookie(result[0].Email, false);
                //if admin
                if (result[0].RoleId == 1)
                {
                    Session["AdminId"] = result[0].UserId;
                }
                //if user
                else if (result[0].RoleId == 2)
                {
                    Session["UserId"] = result[0].UserId;
                }
                ViewBag.Name = result[0].Name;
                return PartialView("_LoginPartial");
            }
            //ViewBag.Message = "Incorrect Email or Password";
            return PartialView("_LoginPartial");
        }

        [HttpGet]
        public ActionResult _NotifyPartial()
        {
            //return PartialView("ListTopics", db.Topics.ToList());
            return PartialView("_NotifyPartial");
        }

        public ActionResult _RegisterPartial()
        {
            ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "Name");
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _RegisterPartial([Bind(Include = "UserId,Email,Name,Password,RoleId")] Users users)
        {
            if (ModelState.IsValid)
            {
                //db.Users.Add(users);
                //db.SaveChanges();
                //Profiles profile = new Profiles
                //{
                //    UserId = users.UserId
                //};
                //db.Profiles.Add(profile);
                //db.SaveChanges();
                //return RedirectToAction("Index", "Home");


                Profiles profile = new Profiles();
                db.Profiles.Add(profile);
                db.SaveChanges();
                users.UserId = profile.UserId;
                db.Users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "Name", users.RoleId);
            return View(users);
        }

        public ActionResult Logout()
        {
            Session["UserId"] = null ;
            Session["AdminId"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home");
        }
    }
}