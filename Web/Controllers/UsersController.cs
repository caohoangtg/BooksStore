using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Data;
using Models;
using Service;
using Web.ViewModels;

namespace Web.Controllers
{
    
    public class UsersController : Controller
    {
        private readonly IUserService userService;
        private readonly IPostService postService;
        private readonly IMarkService markService;
        private readonly IProfileService profileService;
        public UsersController(IUserService userService, IPostService postService, IMarkService markService, IProfileService profileService)
        {
            this.userService = userService;
            this.postService = postService;
            this.markService = markService;
            this.profileService = profileService;
        }

        private ForumEntities db = new ForumEntities();

        // GET: Users
        public ActionResult Index1()
        {
            var users = db.Users.Include(u => u.Role);
            return View(users.ToList());
        }

        [Authorize]
        public ActionResult Index()
        {
            int id = 0;
            if (Session["UserId"] != null)
                id = ((int)Session["UserId"]);
            else if (Session["AdminId"] != null)
                id = ((int)Session["AdminId"]);
            else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //var user = db.Users.Find(id);
            var user = Mapper.Map<Users, UserViewModel>(userService.GetUser(id));
            //ViewBag.LstUser = user;
            return View(user);
        }

        public ActionResult _ProfilePartial()
        {
            int id = 0;
            if (Session["UserId"] != null)
                id = ((int)Session["UserId"]);
            else if (Session["AdminId"] != null)
                id = ((int)Session["AdminId"]);
            else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //var list = db.Posts.Where(u => u.UserId == id).ToList();
            var list = Mapper.Map<IEnumerable<Posts>, IEnumerable<PostViewModel>>(postService.GetPosts().Where(u => u.UserId == id));
            if (list.Count() == 0)
                ViewBag.Message = "Bạn chưa có bài viết nào!";
            ViewBag.List = list;
            return PartialView(list);
        }

        public ActionResult _MarksPartial()
        {
            int id = 0;
            if (Session["UserId"] != null)
                id = ((int)Session["UserId"]);
            else if (Session["AdminId"] != null)
                id = ((int)Session["AdminId"]);
            else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //var list = db.Marks.Where(u => u.UserId == id);
            var list = Mapper.Map<IEnumerable<Marks>, IEnumerable<MarkViewModel>>(markService.GetMarks().Where(u => u.UserId == id));
            if (list.Count() == 0)
                ViewBag.Message = "Bạn chưa đánh dấu bài viết nào!";
            ViewBag.List = list;
            return PartialView(list);
        }

        // GET: Users/Edit/5
        public ActionResult _TimelinePartial()
        {
            int id = 0;
            if (Session["UserId"] != null)
                id = ((int)Session["UserId"]);
            else if (Session["AdminId"] != null)
                id = ((int)Session["AdminId"]);
            else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var profiles = Mapper.Map<Profiles, ProfileViewModel>(profileService.GetProfiles().Where(u => u.UserId == id).FirstOrDefault());
            return PartialView(profiles);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _TimelinePartial([Bind(Include = "ProfileId,UserId,FullName,Phone,ImageData,Birthday,Address")] ProfileViewModel profiles)
        {
            if (Session["UserId"] == null && Session["AdminId"] == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var profile = Mapper.Map<ProfileViewModel, Profiles>(profiles);
            if(profiles.UserId == 0)
            {
                if (Session["UserId"] != null)
                    profile.UserId = ((int)Session["UserId"]);
                else if (Session["AdminId"] != null)
                    profile.UserId = ((int)Session["AdminId"]);
                profileService.CreateProfile(profile);
            }
            else
            {
                profileService.EditProfile(profile);
            }
            profileService.SaveProfile();
            return RedirectToAction("Index");
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "Name");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,Email,Name,Password,RoleId")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "Name", users.RoleId);
            return View(users);
        }

        

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,Email,Name,Password,RoleId")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "Name", users.RoleId);
            return View(users);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users users = db.Users.Find(id);
            db.Users.Remove(users);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
