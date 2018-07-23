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
    public interface ITopicService
    {
        IEnumerable<Topics> GetTopics();
        Topics GetTopic(int? id);
        void CreateTopic(Topics Topic);
        void EditTopic(Topics Topic);
        void SaveTopic();
        void DeleteTopic(int id);
    }

    public class TopicService : ITopicService
    {
        private readonly ITopicRepository TopicRepository;
        private readonly IUnitOfWork unitOfWork;

        public TopicService(ITopicRepository TopicRepository, IUnitOfWork unitOfWork)
        {
            this.TopicRepository = TopicRepository;
            this.unitOfWork = unitOfWork;
        }

        public void CreateTopic(Topics Topic)
        {
            TopicRepository.Add(Topic);
        }

        public void EditTopic(Topics Topic)
        {
            TopicRepository.Edit(Topic);
        }

        public Topics GetTopic(int? id)
        {
            var Topic = TopicRepository.GetById(id);
            return Topic;
        }

        public IEnumerable<Topics> GetTopics()
        {
            var Topics = TopicRepository.GetAll();
            return Topics;
        }

        public void SaveTopic()
        {
            unitOfWork.Commit();
        }
        public void DeleteTopic(int id)
        {
            var Topic = TopicRepository.GetById(id);
            TopicRepository.Delete(Topic);
        }
    }
}
