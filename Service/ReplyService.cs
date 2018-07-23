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
    public interface IReplyService
    {
        IEnumerable<Replys> GetReplys();
        Replys GetReply(int? id);
        void CreateReply(Replys Reply);
        void EditReply(Replys Reply);
        void SaveReply();
        void DeleteReply(int id);
    }

    public class ReplyService : IReplyService
    {
        private readonly IReplyRepository ReplyRepository;
        private readonly IUnitOfWork unitOfWork;

        public ReplyService(IReplyRepository ReplyRepository, IUnitOfWork unitOfWork)
        {
            this.ReplyRepository = ReplyRepository;
            this.unitOfWork = unitOfWork;
        }

        public void CreateReply(Replys Reply)
        {
            ReplyRepository.Add(Reply);
        }

        public void EditReply(Replys Reply)
        {
            ReplyRepository.Edit(Reply);
        }

        public Replys GetReply(int? id)
        {
            var Reply = ReplyRepository.GetById(id);
            return Reply;
        }

        public IEnumerable<Replys> GetReplys()
        {
            var Replys = ReplyRepository.GetAll();
            return Replys;
        }

        public void SaveReply()
        {
            unitOfWork.Commit();
        }
        public void DeleteReply(int id)
        {
            var Reply = ReplyRepository.GetById(id);
            ReplyRepository.Delete(Reply);
        }
    }
}
