using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Data;
using Models;
using Service;
using Web.ViewModels;
using F23.StringSimilarity;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly ICommentService commentService;
        private readonly IMarkService markService;
        private readonly IPostService postService;
        private readonly IProfileService profileService;
        private readonly IReplyService replyService;
        private readonly IRoleService roleService;
        private readonly ITopicService topicService;
        private readonly IUserService userService;

        public HomeController(ICategoryService categoryService, ICommentService commentService, IMarkService markService, IPostService postService, IProfileService profileService, IReplyService replyService, IRoleService roleService, ITopicService topicService, IUserService userService)
        {
            this.categoryService = categoryService;
            this.commentService = commentService;
            this.markService = markService;
            this.postService = postService;
            this.profileService = profileService;
            this.replyService = replyService;
            this.roleService = roleService;
            this.topicService = topicService;
            this.userService = userService;
        }

        public IEnumerable<PostViewModel> GetListPosts()
        {
            return Mapper.Map<IEnumerable<Posts>, IEnumerable<PostViewModel>>(postService.GetPosts());
        }

        public IEnumerable<CategoryViewModel> GetListCategories()
        {
            return Mapper.Map<IEnumerable<Categories>, IEnumerable<CategoryViewModel>>(categoryService.GetCategories());
        }
        public IEnumerable<TopicViewModel> GetListTopics()
        {
            return Mapper.Map<IEnumerable<Topics>, IEnumerable<TopicViewModel>>(topicService.GetTopics());
        }

        public IEnumerable<ReplyViewModel> GetListReplys()
        {
            return Mapper.Map<IEnumerable<Replys>, IEnumerable<ReplyViewModel>>(replyService.GetReplys());
        }

        public IEnumerable<CommentViewModel> GetListComments()
        {
            return Mapper.Map<IEnumerable<Comments>, IEnumerable<CommentViewModel>>(commentService.GetComments());
        }

        public IEnumerable<MarkViewModel> GetListMarks()
        {
            return Mapper.Map<IEnumerable<Marks>, IEnumerable<MarkViewModel>>(markService.GetMarks());
        }

        public IEnumerable<ProfileViewModel> GetListProfiles()
        {
            return Mapper.Map<IEnumerable<Profiles>, IEnumerable<ProfileViewModel>>(profileService.GetProfiles());
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index1()
        {
            return View();
        }

        public int GetNotificationContacts()
        {
            if (Session["AdminId"] != null)
            {
                return 1;
            }
            return 0;
        }

        [HttpGet]
        public ActionResult CheckTitle(string title)
        {
            var jw = new JaroWinkler();
            // double t = jw.Similarity("My string", "My tsring");
            var lstPost = GetListPosts();
            var posts = new List<PostViewModel>();
            foreach (var item in lstPost)
            {
                var check = jw.Similarity(title, item.Title);
                if (check >= 0.7)
                {
                    posts.Add(item);
                }
            }
            if (posts.Count()>0)
            {
                //var viewposts = Mapper.Map<IEnumerable<Posts>, IEnumerable<PostViewModel>>(posts);
                return PartialView("_ListBooksPartial", posts);
            }
            return null;
        }

        [HttpGet]
        public ActionResult ListCategories()
        {
            //return PartialView("ListCategories", db.Categories.ToList());
            return PartialView("ListCategories", GetListCategories().ToList());
        }

        [HttpGet]
        public ActionResult ListTopics()
        {
            //return PartialView("ListTopics", db.Topics.ToList());
            return PartialView("ListTopics", GetListTopics().ToList());
        }

        [HttpGet]
        public ActionResult _PopularPartial()
        {
            return PartialView("_PopularPartial", GetListPosts().OrderByDescending(t => t.View).Take(3));
        }

        [HttpGet]
        public ActionResult _RecentPartial()
        {
            return PartialView("_RecentPartial", GetListPosts().OrderByDescending(t=> t.Time.Day).ThenByDescending(t => t.Time.TimeOfDay).Take(3));
        }

        [HttpGet]
        public ActionResult _NotifyPartial()
        {
            var lstNotify = GetListPosts().OrderByDescending(t => t.Time.Day).ThenByDescending(t=> t.Time.TimeOfDay).Take(3);
            //return PartialView("ListTopics", db.Topics.ToList());
            return PartialView(lstNotify);
        }

        [HttpGet]
        public ActionResult Search(string title)
        {
            IEnumerable<PostViewModel> searchList;
            //searchList = db.Posts.Where(t => t.Title.Contains(title) || title ==null);
            searchList = GetListPosts().Where(t => t.Title.Contains(title) || title == null);
            if (searchList.ToList().Count() == 0)
                ViewBag.Message = "Không tìm thấy kết quả '" + title + "'";
            return PartialView("_ListBooksPartial", searchList.ToList());
        }

        public ActionResult _ListBooksPartial()
        {
            return PartialView(GetListPosts().ToList());
        }

        [HttpGet]
        public ActionResult SingleCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var cate = Mapper.Map<Categories, CategoryViewModel>(categoryService.GetCategory(id));
            if (cate == null)
            {
                return HttpNotFound();
            }
            if (cate.Posts.Count() == 0)
            {
                ViewBag.Message = "Chưa có bài viết nào về chuyên đề này";
            }
            return View("_ListBooksPartial", cate.Posts);
        }

        public ActionResult SingleTopic(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var topics = Mapper.Map<Topics, TopicViewModel>(topicService.GetTopic(id));
            if (topics == null)
            {
                return HttpNotFound();
            }
            if (topics.Posts.Count() == 0)
            {
                ViewBag.Message = "Chưa có bài viết nào về chuyên đề này";
            }
            return View("_ListBooksPartial", topics.Posts);
        }

        [Authorize]
        [HttpGet]
        public ActionResult NewPost()
        {
            ViewBag.CategoryId = new SelectList(GetListCategories(), "CategoryId", "Name");
            ViewBag.TopicId = new SelectList(GetListTopics(), "TopicId", "Name");
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult NewPost([Bind(Include = "PostId,Title,Description,Time,View,File,Check,CategoryId,TopicId,UserId,ImageFile")] PostViewModel posts)
        {
            if(posts.Description != null && posts.Title != null && posts.Title.Length> 16)
            {
                if (ModelState.IsValid)
                {
                    if (Session["UserId"] != null)
                        posts.UserId = ((int)Session["UserId"]);
                    else if (Session["AdminId"] != null)
                        posts.UserId = ((int)Session["AdminId"]);
                    else
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    var file = posts.ImageFile;
                    if (file != null && file.ContentLength > 0)
                    {
                        var filename = Path.GetFileName(file.FileName);
                        file.SaveAs(Server.MapPath("~/App_Data/uploads/" + file.FileName));
                        posts.File = file.FileName;
                    }
                    var post = Mapper.Map<PostViewModel, Posts>(posts);
                    postService.CreatePost(post);
                    postService.SavePost();
                    return RedirectToAction("SinglePost", new { id = post.PostId });
                }
            }
            ViewBag.CategoryId = new SelectList(GetListCategories(), "CategoryId", "Name");
            ViewBag.TopicId = new SelectList(GetListTopics(), "TopicId", "Name");
            return RedirectToAction("NewPost");
        }

        public FileResult DownloadFile(string ImageName)
        {
            var FileVirtualPath = "~/App_Data/uploads/" + ImageName;
            return File(FileVirtualPath, "application/force-download", Path.GetFileName(FileVirtualPath));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult _ReplyPartial([Bind(Include = "ReplyId,CommentId,UserId,RepComment,TimeRep")] ReplyViewModel replys)
        {
            if (Session["UserId"] != null)
                replys.UserId = ((int)Session["UserId"]);
            else if (Session["AdminId"] != null)
                replys.UserId = ((int)Session["AdminId"]);
            else
            {
                ViewBag.Message = "Bạn cần phải đăng nhập";
                return PartialView();
            }
            if (ModelState.IsValid)
            {
                var reply = Mapper.Map<ReplyViewModel, Replys>(replys);
                replyService.CreateReply(reply);
                replyService.SaveReply();
                var r = GetListReplys().Where(i => i.ReplyId == reply.ReplyId).FirstOrDefault(); //replyService.GetReply(reply.ReplyId);
                return PartialView(r);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult _CommentPartial([Bind(Include = "CommentId,PostId,UserId,Comment,TimeComment")] CommentViewModel comments)
        {
            if (Session["UserId"] != null)
                comments.UserId = ((int)Session["UserId"]);
            else if (Session["AdminId"] != null)
                comments.UserId = ((int)Session["AdminId"]);
            else
            {
                ViewBag.Message = "Bạn cần phải đăng nhập";
                return PartialView();
            }

            if (ModelState.IsValid)
            {
                var comment = Mapper.Map<CommentViewModel, Comments>(comments);
                commentService.CreateComment(comment);
                commentService.SaveComment();
                //var r = Mapper.Map<Comments, CommentViewModel>(commentService.GetComment(comment.CommentId));
                var r = GetListComments().Where(i => i.CommentId == comment.CommentId).FirstOrDefault();
                return PartialView(r);
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost]
        public void Marks([Bind(Include = "MarkId,PostId,UserId")] Marks marks)
        {
            if (Session["UserId"] != null)
                marks.UserId = ((int)Session["UserId"]);
            else if (Session["AdminId"] != null)
                marks.UserId = ((int)Session["AdminId"]);
            //var mark = db.Marks.Where(i => i.PostId == marks.PostId && i.UserId == marks.UserId);
            var mark = markService.GetMarks().Where(i => i.PostId == marks.PostId && i.UserId == marks.UserId).FirstOrDefault();
            if (mark == null)
            {
                markService.CreateMark(marks);
                //db.Marks.Add(marks);
            }
            else
            {
                //markService.DeleteMark(mark.Select(m => m.MarkId).FirstOrDefault());
                markService.DeleteMark(mark.PostId, mark.UserId);
                //db.Marks.Remove(mark.FirstOrDefault());
            }
            markService.SaveMark();
            //db.SaveChanges();
            return;
        }

        [HttpGet]
        public int CheckMarks(int id)
        {
            var idU = 0;
            if (Session["UserId"] != null)
                idU = ((int)Session["UserId"]);
            else if (Session["AdminId"] != null)
                idU = ((int)Session["AdminId"]);
            //var mark = db.Marks.Where(i => i.PostId == id && i.UserId == idU);
            var mark = GetListMarks().Where(i => i.PostId == id && i.UserId == idU);
            if (mark.Count() == 0)
            {
                return 0;
            }
            return 1;
        }

        [HttpGet]
        public ActionResult SinglePost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Posts posts = db.Posts.Find(id);
            Posts posts = postService.GetPost(id);
            if (posts == null)
            {
                return HttpNotFound();
            }
            posts.View += 1;
            postService.SavePost();
            return View(Mapper.Map<Posts, PostViewModel>(posts));
        }

        [HttpPost]
        public void Upload(HttpPostedFileWrapper ImageFile)
        {
            var idU = 0;
            if (Session["UserId"] != null)
                idU = ((int)Session["UserId"]);
            else if (Session["AdminId"] != null)
                idU = ((int)Session["AdminId"]);
            var file = ImageFile;
            if (file != null && file.ContentLength > 0)
            {
                //var filename = Path.GetFileName(file.FileName);
                file.SaveAs(Server.MapPath("~/Content/Images/avatar/" + file.FileName));
                Profiles profile = profileService.GetProfiles().Where(i => i.UserId == idU).FirstOrDefault();
                if (profile != null)
                {
                    profile.ImageData = file.FileName;
                    //var prof = Mapper.Map<ProfileViewModel, Profiles>(profile);
                    //profileService.EditProfile(prof);
                    profileService.SaveProfile();
                }
            }
            return;
        }

        [Authorize]
        [HttpGet]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostViewModel posts = Mapper.Map<Posts, PostViewModel>(postService.GetPost(id)); //db.Posts.Find(id);
            if (posts == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(GetListCategories(), "CategoryId", "Name", posts.CategoryId);
            ViewBag.TopicId = new SelectList(GetListTopics(), "TopicId", "Name", posts.TopicId);
            return View(posts);
        }

        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditPost([Bind(Include = "PostId,Title,Description,Time,View,CategoryId,TopicId,UserId")] PostViewModel posts)
        {
            if (ModelState.IsValid)
            {
                var post = Mapper.Map<PostViewModel, Posts>(posts);
                postService.EditPost(post);
                postService.SavePost();
                return RedirectToAction("Index", "Users");

                //db.Entry(posts).State = EntityState.Modified;
                //db.SaveChanges();
                //return RedirectToAction("Index");
            }
            return RedirectToAction("SinglePost", "Home");
        }

        [Authorize]
        [HttpPost]
        public int DeletePost(int id)
        {
            postService.DeletePost(id);
            postService.SavePost();
            return 1;
        }
    }
}