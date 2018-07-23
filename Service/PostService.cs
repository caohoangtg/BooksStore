using Data.Infrastructure;
using Data.Repositories;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IPostService
    {
        IEnumerable<Posts> GetPosts();
        Posts GetPost(int? id);
        void CreatePost(Posts Post);
        void EditPost(Posts Post);
        void SavePost();
        void DeletePost(int id);
    }

    public class PostService : IPostService
    {
        private readonly IPostRepository PostRepository;
        private readonly IUnitOfWork unitOfWork;

        public PostService(IPostRepository PostRepository, IUnitOfWork unitOfWork)
        {
            this.PostRepository = PostRepository;
            this.unitOfWork = unitOfWork;
        }

        public void CreatePost(Posts Post)
        {
            PostRepository.Add(Post);
        }

        public void EditPost(Posts Post)
        {
            PostRepository.Edit(Post);
        }

        public Posts GetPost(int? id)
        {
            var Post = PostRepository.GetById(id);
            return Post;
        }

        public IEnumerable<Posts> GetPosts()
        {
            var Posts = PostRepository.GetAll();
            return Posts;
        }

        public void SavePost()
        {
            unitOfWork.Commit();
        }
        public void DeletePost(int id)
        {
            var Post = PostRepository.GetById(id);
            PostRepository.Delete(Post);
        }
    }
}
