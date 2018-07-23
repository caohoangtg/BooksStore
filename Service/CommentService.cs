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
    public interface ICommentService
    {
        IEnumerable<Comments> GetComments();
        Comments GetComment(int? id);
        void CreateComment(Comments Comment);
        void EditComment(Comments Comment);
        void SaveComment();
        void DeleteComment(int id);
    }

    public class CommentService : ICommentService
    {
        private readonly ICommentRepository CommentRepository;
        private readonly IUnitOfWork unitOfWork;

        public CommentService(ICommentRepository CommentRepository, IUnitOfWork unitOfWork)
        {
            this.CommentRepository = CommentRepository;
            this.unitOfWork = unitOfWork;
        }

        public void CreateComment(Comments Comment)
        {
            CommentRepository.Add(Comment);
        }

        public void EditComment(Comments Comment)
        {
            CommentRepository.Edit(Comment);
        }

        public Comments GetComment(int? id)
        {
            var Comment = CommentRepository.GetById(id);
            return Comment;
        }
        
        public IEnumerable<Comments> GetComments()
        {
            var Comments = CommentRepository.GetAll();
            return Comments;
        }

        public void SaveComment()
        {
            unitOfWork.Commit();
        }
        public void DeleteComment(int id)
        {
            var Comment = CommentRepository.GetById(id);
            CommentRepository.Delete(Comment);
        }
    }
}
